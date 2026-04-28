using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public sealed class ConsumableTargetSelectorSystem : IPausableTick
    {
        private readonly IConsumerService _consumerService;
        private readonly IConsumableService _consumableService;
        
        public ConsumableTargetSelectorSystem(
            IConsumerService consumerService,
            IConsumableService consumableService)
        {
            _consumerService = consumerService;
            _consumableService = consumableService;
        }
        
        public void PausableTick()
        {
            foreach (var consumer in _consumerService.Consumers)
            {
                var target = _consumerService.GetConsumableTarget(consumer);
                if (target == EntityGUID.Null ||
                    !_consumableService.IsConsumable(target) ||
                    _consumableService.IsConsumed(target))
                {
                    _consumerService.SelectConsumableTarget(consumer);
                }
            }
        }
    }
}