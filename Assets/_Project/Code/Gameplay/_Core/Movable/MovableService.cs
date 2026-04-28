using System.Collections.Generic;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;

namespace _Project.Code.Gameplay._Core.Movable
{
    public sealed class MovableService : IMovableService
    {
        private readonly Dictionary<EntityGUID, IMovable> _entities = new();

        public void Register(IMovable movable)
        {
            if (movable == null || movable.GUID == EntityGUID.Null || _entities.ContainsKey(movable.GUID))
                return;
            _entities.Add(movable.GUID, movable);
        }

        public void Unregister(EntityGUID guid)
        {
            _entities.Remove(guid);
        }

        public void MoveAll()
        {
            foreach (var movable in _entities.Values)
            {
                movable.MovementRule.Execute(movable);
            }
        }
    }
}