using _Project.Code.Core.Keys;
using _Project.Code.Gameplay._Core;
using _Project.Code.Local;
using _Project.Code.StaticData;
using UnityEngine;

namespace _Project.Code.Gameplay.CoreFeatures.BugColony
{
    public sealed class FoodFactory : IFoodFactory
    {
        private readonly BugColonyStaticData _staticData;
        private readonly IBugColonyService _bugColonyService;
        private readonly IConsumableService _consumableService;
        private readonly IEntityGuidService _entityGuidService;
        private readonly IDestructibleService _destructibleService;
        private readonly ICirclePhysicsService _physicsService;
        private readonly IMemoryPoolService _memoryPoolService;
        private readonly IEntityViewService _entityViewService;
        
        public FoodFactory(
            StaticDataService staticDataService,
            IBugColonyService bugColonyService,
            IDestructibleService destructibleService,
            ICirclePhysicsService physicsService,
            IConsumableService consumableService,
            IEntityGuidService entityGuidService,
            IMemoryPoolService memoryPoolService,
            IEntityViewService entityViewService)
        {
            _staticData = staticDataService.BugColonyStaticData;
            _bugColonyService = bugColonyService;
            _destructibleService = destructibleService;
            _physicsService = physicsService;
            _consumableService = consumableService;
            _entityGuidService = entityGuidService;
            _memoryPoolService = memoryPoolService;
            _entityViewService = entityViewService;
        }
        
        public FoodEntity CreateAndRegister(FoodId foodId, Vector2 worldPosition)
        {
            var guid = _entityGuidService.NewGuid();
            var config = _staticData.GetFoodConfig(foodId);

            var food = new FoodEntity(
                guid,
                config.ConsumeReward,
                config.ConsumableRule,
                config.DestroyRule,
                config.ContactRules,
                config.IsStaticBody,
                config.ColliderRadius,
                worldPosition);

            _bugColonyService.RegisterFood(food, guid);
            _destructibleService.Register(food, guid);
            _consumableService.Register(food, guid);
            _physicsService.Register(food);
            
            var view = _memoryPoolService.SpawnGameObject<EntityView>(config.ViewPoolId);
            view.Bind(guid, config.ViewPoolId, worldPosition);
            _entityViewService.Register(view, guid);
            
            return food;
        }
    }
}