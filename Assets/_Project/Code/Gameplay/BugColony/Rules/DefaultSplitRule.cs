using System;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Events;
using _Project.Code.Core.Keys;
using UnityEngine;
using Zenject;

namespace _Project.Code.Gameplay.CoreFeatures.BugColony
{
    [Serializable]
    public sealed class DefaultSplitRule : ISplitRule
    {
        [SerializeField] private int _consumptionToSplit;
        [SerializeField] private BugId _bugId;
        [SerializeField] private int _splitCount;
        
        [Inject] private readonly IBugFactory _bugFactory;
        [Inject] private readonly IDestructibleService _destructibleService;
        [Inject] private readonly ICirclePhysicsService _physicsService;
        
        public void Execute(ConsumptionUpdateEvent @event)
        {
            if (@event.Consumption < _consumptionToSplit)
                return;

            var position = Vector2.zero;
            if (_physicsService.TryGetBody(@event.Entity, out var body))
                position = body.WorldPosition;
            
            if (_destructibleService.IsDestructible(@event.Entity))
                _destructibleService.Destroy(DestroyCause.Split, @event.Entity);
            
            for (int i = 0; i < _splitCount; i++)
                _bugFactory.CreateAndRegister(_bugId, position);
        }
    }
}