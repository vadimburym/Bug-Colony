using System.Collections.Generic;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public sealed class LifeTimeService : ILifeTimeService
    {
        private readonly Dictionary<EntityGUID, IHasLifeTime> _entities = new();
        
        private readonly List<EntityGUID> _updateBuffer = new();
        
        public void Register(IHasLifeTime entity, EntityGUID guid)
        {
            if (entity == null || guid.IsNull || _entities.ContainsKey(guid))
                return;
            _entities.Add(guid, entity);
        }
        
        public bool HasLifeTime(EntityGUID guid) => _entities.ContainsKey(guid);
        
        public void UpdateLifeTime(EntityGUID guid, float deltaTime)
        {
            var entity = _entities[guid];
            if (entity.TryUpdateLifetime(deltaTime, out var @event))
            {
                entity.LifeTimeRule?.Execute(@event);
            }
        }
        
        public void UpdateAllLifeTime(float deltaTime)
        {
            _updateBuffer.Clear();
            foreach (var entity in _entities)
                _updateBuffer.Add(entity.Key);

            for (int i = 0; i < _updateBuffer.Count; i++)
            {
                var entity = _entities[_updateBuffer[i]];
                if (entity.TryUpdateLifetime(deltaTime, out var @event))
                {
                    entity.LifeTimeRule?.Execute(@event);
                }
            }
        }
        
        public void Unregister(EntityGUID guid) => _entities.Remove(guid);
    }
}