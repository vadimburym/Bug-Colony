using _Project.Code.Core.Abstractions;
using _Project.Code.Infrastructure;
using _Project.Code.StaticData;
using UnityEngine;
using Zenject;

namespace _Project.Code.GameApp
{
    public sealed class BootstrapInstaller : MonoInstaller
    {
        [SerializeReference] private ISaveStrategy _saveStrategy;
        [SerializeField] private LoadingCurtainProvider _loadingCurtain;
        [SerializeField] private AudioProvider _audioProvider;
        [SerializeField] private StaticDataService _staticDataService;
        [SerializeField] private GameUIInstallers _gameUIInstallers;
        
        public override void InstallBindings()
        {
            Container.Bind<StaticDataService>().FromInstance(_staticDataService).AsSingle();
            Container.Bind<GameUIInstallers>().FromInstance(_gameUIInstallers).AsSingle();
            
            BindInfrastructure(Container);
            BindGameStates(Container);
            
            Container.BindInterfacesTo<BootstrapEntryPoint>().AsSingle();
        }

        private void BindGameStates(DiContainer container)
        {
            container.Bind<IGameState>().To<GameplayState>().AsSingle();  
        }

        private void BindInfrastructure(DiContainer container)
        {
            container.Bind<IAddressableService>().To<AddressableService>().AsSingle();
            container.Bind<IStateMachine>().To<StateMachine>().AsSingle();
            container.Bind<ILocalContextService>().To<LocalContextService>().AsSingle();
            container.Bind<ISceneLoadService>().To<SceneLoadService>().AsSingle();
            container.Bind<ISaveRepository>().To<SaveRepository>().AsSingle().WithArguments(_saveStrategy);
            container.Bind<IGamePauseService>().To<GamePauseService>().AsSingle();
            container.Bind<IMainCameraService>().To<MainCameraService>().AsSingle();
            container.Bind<ILoadingCurtainProvider>().To<LoadingCurtainProvider>().FromInstance(_loadingCurtain).AsSingle();
            container.Bind<IAudioProvider>().To<AudioProvider>().FromInstance(_audioProvider).AsSingle();
            container.Bind<IRandomService>().To<RandomService>().AsSingle();
        }
    }
}