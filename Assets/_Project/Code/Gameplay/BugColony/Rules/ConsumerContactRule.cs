using System;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Events;
using _Project.Code.Gameplay.CoreFeatures;
using Zenject;

namespace _Project.Code.Gameplay.BugColony.Rules
{
    [Serializable]
    public class ConsumerContactRule : IPhysicsContactRule
    {
        [Inject] private readonly IConsumableService _consumableService;
        [Inject] private readonly IConsumerService _consumerService;
        
        public void Execute(PhysicsContactEvent @event)
        {
            if (!_consumerService.IsConsumer(@event.Self))
                return;
            if (!_consumableService.IsConsumable(@event.Collider))
                return;
            if (_consumerService.GetConsumableTarget(@event.Self) != @event.Collider)
                return;
            _consumableService.Consume(@event.Self, @event.Collider);
        }
    }
}