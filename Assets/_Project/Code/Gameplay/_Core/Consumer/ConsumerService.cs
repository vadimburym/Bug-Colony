using System.Collections.Generic;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public sealed class ConsumerService : IConsumerService
    {
        public IReadOnlyCollection<EntityGUID> Consumers => _entities.Keys;
        
        private readonly Dictionary<EntityGUID, IConsumer> _entities = new();

        public void Register(IConsumer consumer, EntityGUID guid)
        {
            if (consumer == null || guid.IsNull || _entities.ContainsKey(guid))
                return;
            _entities.Add(guid, consumer);
        }
        
        public bool IsConsumer(EntityGUID guid) => _entities.ContainsKey(guid);

        public int GetConsumptionStorage(EntityGUID guid) => _entities[guid].ConsumptionStorage;
        
        public EntityGUID GetConsumableTarget(EntityGUID guid) => _entities[guid].ConsumableTarget;
        
        public void SelectConsumableTarget(EntityGUID guid)
        {
            var entity = _entities[guid];
            entity.SelectorRule.Execute(entity, guid);
        }

        public void IncreaseConsumption(EntityGUID guid, int amount)
        {
            var entity = _entities[guid];
            if (entity.TryIncreaseConsumption(amount, out var @event))
            {
                entity.SplitRule?.Execute(@event);
            }
        }

        public void DecreaseConsumption(EntityGUID guid, int amount)
        {
            var entity = _entities[guid];
            if (entity.TryDecreaseConsumption(amount, out var @event))
            {
                entity.SplitRule?.Execute(@event);
            }
        }

        public void ResetConsumption(EntityGUID guid)
        {
            var entity = _entities[guid];
            if (entity.TryResetConsumption(out var @event))
            {
                entity.SplitRule?.Execute(@event);
            }
        }
        
        public void Unregister(EntityGUID guid) => _entities.Remove(guid);
    }
}