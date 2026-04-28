using System;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Events;
using _Project.Code.Gameplay._Core;
using _Project.Code.Gameplay.CoreFeatures.BugColony;
using _Project.Code.Local;
using Zenject;

namespace _Project.Code.Gameplay.CoreFeatures
{
    [Serializable]
    public sealed class FoodDestroyRule : IDestroyRule
    {
        [Inject] private readonly IBugColonyService _bugColonyService;
        [Inject] private readonly IConsumableService _consumableService;
        [Inject] private readonly IDestructibleService _destructibleService;
        [Inject] private readonly ICirclePhysicsService _physicsService;
        [Inject] private readonly IMemoryPoolService _memoryPoolService;
        [Inject] private readonly IEntityViewService _entityViewService;
        
        public void Execute(DestroyEvent @event)
        {
            if (!_bugColonyService.IsFood(@event.Entity))
                return;
            
            _consumableService.Unregister(@event.Entity);
            _destructibleService.Unregister(@event.Entity);
            _physicsService.Unregister(@event.Entity);
            _bugColonyService.UnregisterFood(@event.Entity);
            
            if (_entityViewService.TryGetView(@event.Entity, out var view))
            {
                view.Unbind();
                _entityViewService.Unregister(@event.Entity);
                _memoryPoolService.UnspawnGameObject(view.PoolId, view);
            }
        }
    }
}