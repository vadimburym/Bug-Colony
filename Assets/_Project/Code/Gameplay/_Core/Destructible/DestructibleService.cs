using System.Collections.Generic;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public sealed class DestructibleService : IDestructibleService
    {
        private readonly Dictionary<EntityGUID, IDestructible> _entities = new();
        
        public void Register(IDestructible entity, EntityGUID guid)
        {
            if (entity == null || guid.IsNull || _entities.ContainsKey(guid))
                return;
            _entities.Add(guid, entity);
        }

        public bool IsDestructible(EntityGUID guid) => _entities.ContainsKey(guid);
        
        public void Destroy(DestroyCause cause, EntityGUID guid)
        {
            var entity = _entities[guid];
            if (entity.TryDestroy(cause, out var @event))
            {
                entity.DestroyRule?.Execute(@event);
            }
        }
        
        public void Unregister(EntityGUID guid) => _entities.Remove(guid);
    }
}