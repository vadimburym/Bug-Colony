using _Project.Code.Core.Keys;
using _Project.Code.Gameplay._Core;
using _Project.Code.Gameplay._Core.Movable;
using _Project.Code.Local;
using _Project.Code.StaticData;
using UnityEngine;

namespace _Project.Code.Gameplay.CoreFeatures.BugColony
{
    public sealed class BugFactory : IBugFactory
    {
        private readonly BugColonyStaticData _staticData;
        private readonly IBugColonyService _bugColonyService;
        private readonly IConsumableService _consumableService;
        private readonly IConsumerService _consumerService;
        private readonly IEntityGuidService _entityGuidService;
        private readonly ILifeTimeService _lifeTimeService;
        private readonly IDestructibleService _destructibleService;
        private readonly ICirclePhysicsService _physicsService;
        private readonly IMovableService _movableService;
        private readonly IMemoryPoolService _memoryPoolService;
        private readonly IEntityViewService _entityViewService;
        
        public BugFactory(
            StaticDataService staticDataService,
            IBugColonyService bugColonyService,
            IConsumableService consumableService,
            IConsumerService consumerService,
            IEntityGuidService entityGuidService,
            ILifeTimeService lifeTimeService,
            IDestructibleService destructibleService,
            ICirclePhysicsService physicsService,
            IMovableService movableService,
            IMemoryPoolService memoryPoolService,
            IEntityViewService entityViewService)
        {
            _staticData = staticDataService.BugColonyStaticData;
            _bugColonyService = bugColonyService;
            _consumableService = consumableService;
            _consumerService = consumerService;
            _entityGuidService = entityGuidService;
            _lifeTimeService = lifeTimeService;
            _destructibleService = destructibleService;
            _physicsService = physicsService;
            _movableService = movableService;
            _memoryPoolService = memoryPoolService;
            _entityViewService = entityViewService;
        }
        
        public BugEntity CreateAndRegister(BugId bugId, Vector2 worldPosition)
        {
            var guid = _entityGuidService.NewGuid();
            var config = _staticData.GetBugConfig(bugId);

            var bug = new BugEntity(
                guid,
                bugId,
                config.MoveSpeed,
                config.ConsumeReward,
                config.ColliderRadius,
                config.IsStaticBody,
                config.ContactRules,
                config.SelectorRule,
                config.SplitRule,
                config.LifeTimeRule,
                config.DestroyRule,
                config.ConsumableRule,
                config.MovementRule,
                worldPosition);
            
            _bugColonyService.RegisterBug(bug, guid);
            _consumableService.Register(bug, guid);
            _consumerService.Register(bug, guid);
            _destructibleService.Register(bug, guid);
            _lifeTimeService.Register(bug, guid);
            _physicsService.Register(bug);
            _movableService.Register(bug);

            var view = _memoryPoolService.SpawnGameObject<EntityView>(config.ViewPoolId);
            view.Bind(guid, config.ViewPoolId, worldPosition);
            _entityViewService.Register(view, guid);
            
            return bug;
        }
    }
}