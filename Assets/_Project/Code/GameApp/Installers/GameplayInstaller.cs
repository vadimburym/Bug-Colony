using _Project.Code.Core.Abstractions;
using _Project.Code.Gameplay._Core;
using _Project.Code.Gameplay._Core.Movable;
using _Project.Code.Gameplay.CoreFeatures;
using _Project.Code.Gameplay.CoreFeatures.BugColony;
using _Project.Code.Local;
using Zenject;

namespace _Project.Code.GameApp
{
    public sealed class GameplayInstaller : Installer<GameplayInstaller>
    {
        public override void InstallBindings()
        {
            BindLocal(Container);
            BindCore(Container);
            BindBugColony(Container);
            BindStatistic(Container);
        }
        
        private static void BindLocal(DiContainer container)
        {
            container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            container.Bind<IMemoryPoolService>().To<MemoryPoolService>().AsSingle();
            container.Bind<IWarmUp>().To<MemoryPoolWarmUpSystem>().AsSingle();
            container.Bind<IWidgetService>().To<WidgetService>().AsSingle();
        }
        
        private static void BindCore(DiContainer container)
        {
            container.Bind<IConsumableService>().To<ConsumableService>().AsSingle();
            container.Bind<IConsumerService>().To<ConsumerService>().AsSingle();
            container.Bind<IDestructibleService>().To<DestructibleService>().AsSingle();
            container.Bind<IEntityGuidService>().To<EntityGuidService>().AsSingle();
            container.Bind<ILifeTimeService>().To<LifeTimeService>().AsSingle();
            container.Bind<ICirclePhysicsService>().To<CirclePhysicsService>().AsSingle();
            container.Bind<IMovableService>().To<MovableService>().AsSingle();
            container.Bind<IEntityViewService>().To<EntityViewService>().AsSingle();
            
            container.Bind<IPausableTick>().To<LifeTimeUpdateSystem>().AsSingle();
            container.Bind<IPausableTick>().To<MoveSystem>().AsSingle();
            container.Bind<IPausableTick>().To<CirclePhysicsSimulateSystem>().AsSingle();
            container.Bind<IPausableTick>().To<ConsumableTargetSelectorSystem>().AsSingle();
            container.Bind<ILateTick>().To<EntityViewPositionSyncSystem>().AsSingle();
        }
        
        private static void BindBugColony(DiContainer container)
        {
            container.Bind<IBugColonyService>().To<BugColonyService>().AsSingle();
            container.Bind<IBugFactory>().To<BugFactory>().AsSingle();
            container.Bind<IFoodFactory>().To<FoodFactory>().AsSingle();
            
            container.Bind<IWarmUp>().To<BugColonyInjectSystem>().AsSingle();
            container.Bind<IPausableTick>().To<BugsSpawnSystem>().AsSingle();
            container.Bind<IPausableTick>().To<FoodSpawnSystem>().AsSingle();
        }
        
        private static void BindStatistic(DiContainer container)
        {
            container.Bind<IStatisticService>().To<StatisticService>().AsSingle();
        }
    }
}