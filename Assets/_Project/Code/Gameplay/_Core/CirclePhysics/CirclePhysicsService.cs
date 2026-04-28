using System.Collections.Generic;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Events;
using _Project.Code.Core.Keys;
using UnityEngine;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public sealed class CirclePhysicsService : ICirclePhysicsService
    {
        private const float EPSILON = 0.0001f;
        private const int SOLVER_ITERATIONS = 2;

        public IReadOnlyList<PhysicsContactEvent> Contacts => _contacts;

        private readonly Dictionary<EntityGUID, ICirclePhysicsBody> _bodies = new();
        private readonly List<ICirclePhysicsBody> _bodyBuffer = new();
        private readonly List<PhysicsContactEvent> _contacts = new();
        
        private readonly Dictionary<Vector2Int, List<ICirclePhysicsBody>> _spatialGrid = new();
        private readonly List<Vector2Int> _occupiedCells = new();
        private float _cellSize = 1f;
        
        public void Register(ICirclePhysicsBody body)
        {
            if (body == null || body.GUID.IsNull || _bodies.ContainsKey(body.GUID))
                return;

            _bodies.Add(body.GUID, body);
        }

        public bool TryGetBody(EntityGUID entityGUID, out ICirclePhysicsBody body)
        {
            return _bodies.TryGetValue(entityGUID, out body);
        }
        
        public void Unregister(EntityGUID guid) 
            => _bodies.Remove(guid);
        
        public void Simulate(float deltaTime, Vector2 minCorner, Vector2 maxCorner)
        {
            _contacts.Clear();
            if (deltaTime <= 0f)
                return;

            FillBodyBuffer();
            Integrate(deltaTime);

            for (int iteration = 0; iteration < SOLVER_ITERATIONS; iteration++)
            {
                RebuildSpatialGrid();
                SolvePotentialPairs(collectContacts: iteration == 0);
            }

            SolveBounds(minCorner, maxCorner);
        }

        private void FillBodyBuffer()
        {
            _bodyBuffer.Clear();
            foreach (var body in _bodies.Values)
                _bodyBuffer.Add(body);
        }

        private void Integrate(float deltaTime)
        {
            for (int i = 0; i < _bodyBuffer.Count; i++)
            {
                var body = _bodyBuffer[i];
                if (body.IsStaticBody)
                    continue;

                body.SetWorldPosition(body.WorldPosition + body.Velocity * deltaTime);
            }
        }

        private void RebuildSpatialGrid()
        {
            _spatialGrid.Clear();
            _occupiedCells.Clear();
            _cellSize = CalculateCellSize();

            for (int i = 0; i < _bodyBuffer.Count; i++)
            {
                var body = _bodyBuffer[i];
                if (!_bodies.ContainsKey(body.GUID))
                    continue;

                var cell = ToCell(body.WorldPosition);
                if (!_spatialGrid.TryGetValue(cell, out var cellBodies))
                {
                    cellBodies = new List<ICirclePhysicsBody>(8);
                    _spatialGrid.Add(cell, cellBodies);
                    _occupiedCells.Add(cell);
                }

                cellBodies.Add(body);
            }
        }

        private float CalculateCellSize()
        {
            var maxRadius = EPSILON;
            for (int i = 0; i < _bodyBuffer.Count; i++)
            {
                var radius = _bodyBuffer[i].ColliderRadius;
                if (radius > maxRadius)
                    maxRadius = radius;
            }

            return Mathf.Max(maxRadius * 2f, EPSILON);
        }

        private Vector2Int ToCell(Vector2 position)
        {
            return new Vector2Int(
                Mathf.FloorToInt(position.x / _cellSize),
                Mathf.FloorToInt(position.y / _cellSize));
        }

        private void SolvePotentialPairs(bool collectContacts)
        {
            for (int cellIndex = 0; cellIndex < _occupiedCells.Count; cellIndex++)
            {
                var cell = _occupiedCells[cellIndex];
                if (!_spatialGrid.TryGetValue(cell, out var cellBodies))
                    continue;

                SolvePairsInsideCell(cellBodies, collectContacts);
                SolvePairsWithForwardNeighborCells(cell, cellBodies, collectContacts);
            }
        }

        private void SolvePairsInsideCell(IReadOnlyList<ICirclePhysicsBody> cellBodies, bool collectContacts)
        {
            for (int i = 0; i < cellBodies.Count; i++)
            {
                var a = cellBodies[i];
                if (!_bodies.ContainsKey(a.GUID))
                    continue;

                for (int j = i + 1; j < cellBodies.Count; j++)
                {
                    var b = cellBodies[j];
                    if (!_bodies.ContainsKey(b.GUID))
                        continue;

                    SolvePair(a, b, collectContacts);
                }
            }
        }

        private void SolvePairsWithForwardNeighborCells(Vector2Int cell, IReadOnlyList<ICirclePhysicsBody> cellBodies, bool collectContacts)
        {
            for (int offsetY = -1; offsetY <= 1; offsetY++)
            {
                for (int offsetX = -1; offsetX <= 1; offsetX++)
                {
                    if (offsetX == 0 && offsetY == 0)
                        continue;

                    var neighbor = new Vector2Int(cell.x + offsetX, cell.y + offsetY);
                    if (!IsForwardNeighbor(cell, neighbor))
                        continue;

                    if (!_spatialGrid.TryGetValue(neighbor, out var neighborBodies))
                        continue;

                    SolvePairsBetweenCells(cellBodies, neighborBodies, collectContacts);
                }
            }
        }

        private static bool IsForwardNeighbor(Vector2Int cell, Vector2Int neighbor)
        {
            return neighbor.y > cell.y || neighbor.y == cell.y && neighbor.x > cell.x;
        }

        private void SolvePairsBetweenCells(IReadOnlyList<ICirclePhysicsBody> cellBodies, IReadOnlyList<ICirclePhysicsBody> neighborBodies, bool collectContacts)
        {
            for (int i = 0; i < cellBodies.Count; i++)
            {
                var a = cellBodies[i];
                if (!_bodies.ContainsKey(a.GUID))
                    continue;

                for (int j = 0; j < neighborBodies.Count; j++)
                {
                    var b = neighborBodies[j];
                    if (!_bodies.ContainsKey(b.GUID))
                        continue;

                    SolvePair(a, b, collectContacts);
                }
            }
        }

        private void SolvePair(ICirclePhysicsBody a, ICirclePhysicsBody b, bool collectContacts)
        {
            var delta = b.WorldPosition - a.WorldPosition;
            var minDistance = a.ColliderRadius + b.ColliderRadius;
            var sqrDistance = delta.sqrMagnitude;

            if (sqrDistance >= minDistance * minDistance)
                return;

            var distance = Mathf.Sqrt(sqrDistance);
            var normal = distance > EPSILON ? delta / distance : BuildStableNormal(a.GUID, b.GUID);
            var penetration = minDistance - distance;

            if (collectContacts)
            {
                var @event = new PhysicsContactEvent(a.GUID, b.GUID, normal, penetration);
                _contacts.Add(@event);
                ExecuteContactRules(a, b, normal, penetration);
                ExecuteContactRules(b, a, -normal, penetration);
            }

            ResolvePosition(a, b, normal, penetration);
            ResolveVelocity(a, b, normal);
        }

        private void ExecuteContactRules(ICirclePhysicsBody p0, ICirclePhysicsBody p1, Vector2 normal, float penetration)
        {
            var rules = p0.ContactRules;
            for (int i = 0; i < rules.Length; i++)
                rules[i].Execute(new PhysicsContactEvent(p0.GUID, p1.GUID, normal, penetration));
        }

        private static Vector2 BuildStableNormal(EntityGUID a, EntityGUID b)
        {
            var seed = (a.Value * 73856093L) ^ (b.Value * 19349663L);
            var angleSeed = Mathf.Abs((int)(seed % 3600L));
            var angle = angleSeed * Mathf.Deg2Rad * 0.1f;
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }

        private static void ResolvePosition(ICirclePhysicsBody a, ICirclePhysicsBody b, Vector2 normal, float penetration)
        {
            var invMassA = a.IsStaticBody ? 0f : 1f;
            var invMassB = b.IsStaticBody ? 0f : 1f;
            var invMassSum = invMassA + invMassB;
            if (invMassSum <= 0f)
                return;

            var correction = normal * (penetration / invMassSum);
            if (!a.IsStaticBody)
                a.SetWorldPosition(a.WorldPosition - correction * invMassA);
            if (!b.IsStaticBody)
                b.SetWorldPosition(b.WorldPosition + correction * invMassB);
        }

        private static void ResolveVelocity(ICirclePhysicsBody a, ICirclePhysicsBody b, Vector2 normal)
        {
            var invMassA = a.IsStaticBody ? 0f : 1f;
            var invMassB = b.IsStaticBody ? 0f : 1f;
            var invMassSum = invMassA + invMassB;
            if (invMassSum <= 0f)
                return;

            var relativeVelocity = b.Velocity - a.Velocity;
            var velocityAlongNormal = Vector2.Dot(relativeVelocity, normal);

            if (velocityAlongNormal >= 0f)
                return;

            var impulse = -velocityAlongNormal / invMassSum;
            if (!a.IsStaticBody)
                a.SetVelocity(a.Velocity - normal * impulse * invMassA);
            if (!b.IsStaticBody)
                b.SetVelocity(b.Velocity + normal * impulse * invMassB);
        }

        private void SolveBounds(Vector2 minCorner, Vector2 maxCorner)
        {
            for (int i = 0; i < _bodyBuffer.Count; i++)
            {
                var body = _bodyBuffer[i];
                if (body.IsStaticBody || !_bodies.ContainsKey(body.GUID))
                    continue;

                var position = body.WorldPosition;
                var velocity = body.Velocity;
                var radius = body.ColliderRadius;

                var minX = minCorner.x + radius;
                var maxX = maxCorner.x - radius;
                var minY = minCorner.y + radius;
                var maxY = maxCorner.y - radius;

                if (position.x < minX)
                {
                    position.x = minX;
                    if (velocity.x < 0f) velocity.x = 0f;
                }
                else if (position.x > maxX)
                {
                    position.x = maxX;
                    if (velocity.x > 0f) velocity.x = 0f;
                }

                if (position.y < minY)
                {
                    position.y = minY;
                    if (velocity.y < 0f) velocity.y = 0f;
                }
                else if (position.y > maxY)
                {
                    position.y = maxY;
                    if (velocity.y > 0f) velocity.y = 0f;
                }

                body.SetWorldPosition(position);
                body.SetVelocity(velocity);
            }
        }
    }
}
