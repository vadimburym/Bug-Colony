using System;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Events;
using _Project.Code.Core.Keys;
using Zenject;

namespace _Project.Code.Gameplay.CoreFeatures
{
    [Serializable]
    public sealed class DefaultConsumableRule : IConsumableRule
    {
        [Inject] private readonly IConsumerService _consumerService;
        [Inject] private readonly IDestructibleService _destructibleService;
        
        public void Execute(ConsumeEvent @event)
        {
            if (_consumerService.IsConsumer(@event.Consumer))
                _consumerService.IncreaseConsumption(@event.Consumer, @event.ConsumeReward);
            
            if (_destructibleService.IsDestructible(@event.Entity))
                _destructibleService.Destroy(DestroyCause.Eaten, @event.Entity);
        }
    }
}