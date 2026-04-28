using System.Collections.Generic;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Events;
using _Project.Code.Core.Keys;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public class ConsumableService : IConsumableService
    {
        private readonly Dictionary<EntityGUID, IConsumable> _entities = new();
        
        public void Register(IConsumable entity, EntityGUID guid)
        {
            if (entity == null || guid.IsNull || _entities.ContainsKey(guid))
                return;
            _entities.Add(guid, entity);
        } 
        
        public bool IsConsumable(EntityGUID guid) => _entities.ContainsKey(guid);

        public bool IsConsumed(EntityGUID guid) => _entities[guid].IsConsumed;
        
        public void Consume(EntityGUID consumerId, EntityGUID consumableId)
        {
            var consumable = _entities[consumableId];
            if (consumable.TryConsumeBy(consumerId, out ConsumeEvent @event))
            {
                consumable.ConsumableRule?.Execute(@event);
            }
        }
        
        public void Unregister(EntityGUID guid) => _entities.Remove(guid);
    }
}