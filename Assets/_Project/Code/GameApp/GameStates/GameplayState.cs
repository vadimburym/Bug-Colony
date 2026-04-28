using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;
using _Project.Code.Infrastructure;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace _Project.Code.GameApp
{
    public sealed class GameplayState : IGameState
    {
        private const int GAMEPLAY_SCENE_IDX = 1;
        private readonly string[] ADDRESSABLE_LABELS = {"gameplay", "preload"};
        public GameStateId GameStateId => GameStateId.Gameplay;

        private readonly ISceneLoadService _sceneLoadService;
        private readonly ILocalContextService _localContextService;
        private readonly ILoadingCurtainProvider _loadingCurtainProvider;
        private readonly GameUIInstallers _gameUIInstallers;
        private readonly IAddressableService _addressableService;
        
        private GameplayEntryPoint _entryPoint;
        
        public GameplayState(
            IAddressableService addressableService,
            ISceneLoadService sceneLoadService,
            ILocalContextService localContextService,
            ILoadingCurtainProvider loadingCurtainProvider,
            GameUIInstallers gameUIInstallers)
        {
            _addressableService = addressableService;
            _sceneLoadService = sceneLoadService;
            _localContextService = localContextService;
            _loadingCurtainProvider = loadingCurtainProvider;
            _gameUIInstallers = gameUIInstallers;
        }
        
        public void Enter() => EnterAsync().Forget();
        
        private async UniTask EnterAsync()
        {
            _loadingCurtainProvider.Show();
            await _addressableService.LoadObjectsByLabelsAsync(ADDRESSABLE_LABELS, Addressables.MergeMode.Intersection);
            await _sceneLoadService.LoadSceneAsync(GAMEPLAY_SCENE_IDX);
            var sceneInstaller = _sceneLoadService.FindFirstComponentInRoots<GameplaySceneInstaller>();
            var uiInstaller = _gameUIInstallers.GameplayUIInstaller;
            
            _localContextService.WarmUp(container =>
            {
                GameplayInstaller.Install(container);
                sceneInstaller?.Install(container);
                uiInstaller?.Install(container);
                
                container.BindInterfacesAndSelfTo<GameplayEntryPoint>().AsSingle();
            });
            
            _entryPoint = _localContextService.Context.Resolve<GameplayEntryPoint>();
            _entryPoint.Start();
            _loadingCurtainProvider.Hide();
        }

        public void Tick() => _entryPoint?.Tick();
        
        public void LateTick() => _entryPoint?.LateTick();
        
        public void Dispose() => _entryPoint?.Dispose();
    }
}