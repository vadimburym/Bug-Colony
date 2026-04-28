using System;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;
using _Project.Code.Gameplay.CoreFeatures;
using UnityEngine;
using Zenject;

namespace _Project.Code.Gameplay.BugColony.Rules
{
    [Serializable]
    public sealed class ConsumerMovementRule : IMovementRule
    {
        [Inject] private readonly IConsumerService _consumerService;
        [Inject] private readonly ICirclePhysicsService _physicsService;
        
        public void Execute(IMovable movable)
        {
            if (!_consumerService.IsConsumer(movable.GUID))
                return;
            
            var target = _consumerService.GetConsumableTarget(movable.GUID);

            if (target == EntityGUID.Null || !_physicsService.TryGetBody(target, out var targetBody))
            {
                movable.SetVelocity(Vector2.zero);
                return;
            }
            
            var velocity = (targetBody.WorldPosition - movable.WorldPosition).normalized * movable.MoveSpeed;
            movable.SetVelocity(velocity);
        }
    }
}