using System;
using System.Collections.Generic;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;
using _Project.Code.Gameplay.CoreFeatures.BugColony;
using _Project.Code.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Project.Code.Gameplay.CoreFeatures
{
    [Serializable]
    public sealed class PredatorConsumableSelectorRule : IConsumableSelectorRule
    {
        [SerializeField] private bool _pickRandom;
        [SerializeField] private BugId[] _bugsToEat;
        
        [Inject] private readonly IBugColonyService _bugColonyService;
        [Inject] private readonly ICirclePhysicsService _physicsService;
        [Inject] private readonly IConsumableService _consumableService;
        [Inject] private readonly IRandomService _randomService;
        
        private readonly List<EntityGUID> _buffer = new();
        
        public void Execute(IConsumer consumer, EntityGUID guid)
        {
            if (_pickRandom)
                SelectRandomConsumable(consumer, guid);
            else
                SelectNearestConsumable(consumer, guid);
        }

        private void SelectRandomConsumable(IConsumer consumer, EntityGUID guid)
        {
            _buffer.Clear();
            foreach (var food in _bugColonyService.Foods)
            {
                if (!_consumableService.IsConsumable(food))
                    continue;
                if (_consumableService.IsConsumed(food))
                    continue;
                _buffer.Add(food);
            }

            foreach (var bugId in _bugsToEat)
            {
                foreach (var bug in _bugColonyService.GetAllBugsByType(bugId))
                {
                    if (!_consumableService.IsConsumable(bug))
                        continue;
                    if (_consumableService.IsConsumed(bug))
                        continue;
                    if (bug == guid)
                        continue;
                    _buffer.Add(bug);
                }
            }
            
            if (_buffer.Count == 0)
                return;
            var index = _randomService.Range(0, _buffer.Count);
            consumer.SetConsumableTarget(_buffer[index]);
        }

        private void SelectNearestConsumable(IConsumer consumer, EntityGUID guid)
        {
            var target = EntityGUID.Null;
            
            if (!_physicsService.TryGetBody(guid, out var consumerBody))
                return;
            
            var consumerPosition = consumerBody.WorldPosition;
            var minSqrDistance = float.MaxValue;
            
            foreach (var food in _bugColonyService.Foods)
            {
                if (!_consumableService.IsConsumable(food))
                    continue;
                if (_consumableService.IsConsumed(food))
                    continue;
                if (!_physicsService.TryGetBody(food, out var body))
                    continue;
                
                var targetPosition = body.WorldPosition;
                var sqrDistance = (targetPosition - consumerPosition).sqrMagnitude;
                if (sqrDistance < minSqrDistance)
                {
                    minSqrDistance = sqrDistance;
                    target = food;
                }
            }

            foreach (var bugId in _bugsToEat)
            {
                foreach (var bug in _bugColonyService.GetAllBugsByType(bugId))
                {
                    if (!_consumableService.IsConsumable(bug))
                        continue;
                    if (_consumableService.IsConsumed(bug))
                        continue;
                    if (!_physicsService.TryGetBody(bug, out var body))
                        continue;
                    if (bug == guid)
                        continue;
                    
                    var targetPosition = body.WorldPosition;
                    var sqrDistance = (targetPosition - consumerPosition).sqrMagnitude;
                    if (sqrDistance < minSqrDistance)
                    {
                        minSqrDistance = sqrDistance;
                        target = bug;
                    }
                }
            }
            
            consumer.SetConsumableTarget(target);
        }
    }
}