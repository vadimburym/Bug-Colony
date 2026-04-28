using System;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Events;
using _Project.Code.Core.Keys;
using _Project.Code.Gameplay._Core;
using _Project.Code.Gameplay._Core.Movable;
using _Project.Code.Local;
using Zenject;

namespace _Project.Code.Gameplay.CoreFeatures.BugColony
{
    [Serializable]
    public sealed class BugDestroyRule : IDestroyRule
    {
        [Inject] private readonly IStatisticService _statisticService;
        [Inject] private readonly IBugColonyService _bugColonyService;
        [Inject] private readonly IConsumableService _consumableService;
        [Inject] private readonly IConsumerService _consumerService;
        [Inject] private readonly IDestructibleService _destructibleService;
        [Inject] private readonly ILifeTimeService _lifeTimeService;
        [Inject] private readonly ICirclePhysicsService _physicsService;
        [Inject] private readonly IMovableService _movableService;
        [Inject] private readonly IMemoryPoolService _memoryPoolService;
        [Inject] private readonly IEntityViewService _entityViewService;
        
        public void Execute(DestroyEvent @event)
        {
            if (!_bugColonyService.IsBug(@event.Entity, out var bugId))
                return;
            
            if (@event.Cause != DestroyCause.Split)
                _statisticService.RegisterDeath(bugId);
            
            _consumableService.Unregister(@event.Entity);
            _consumerService.Unregister(@event.Entity);
            _destructibleService.Unregister(@event.Entity);
            _lifeTimeService.Unregister(@event.Entity);
            _physicsService.Unregister(@event.Entity);
            _movableService.Unregister(@event.Entity);
            _bugColonyService.UnregisterBug(@event.Entity);

            if (_entityViewService.TryGetView(@event.Entity, out var view))
            {
                view.Unbind();
                _entityViewService.Unregister(@event.Entity);
                _memoryPoolService.UnspawnGameObject(view.PoolId, view);
            }
        }
    }
}