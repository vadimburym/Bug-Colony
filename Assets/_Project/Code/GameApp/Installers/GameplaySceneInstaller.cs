using _Project.Code.Core.Abstractions;
using _Project.Code.Gameplay.BugColony.Providers;
using _Project.Code.Local;
using UnityEngine;
using Zenject;

namespace _Project.Code.GameApp
{
    public sealed class GameplaySceneInstaller : SceneInstaller
    {
        [SerializeField] private SceneEntryCameraProvider _sceneEntryCameraProvider;
        [SerializeField] private TransformProvider _transformProvider;
        [SerializeField] private GroundAreaProvider _groundAreaProvider;
        
        public override void Install(DiContainer container)
        {
            container.Bind<IGroundAreaProvider>().To<GroundAreaProvider>().FromInstance(_groundAreaProvider).AsSingle();
            container.Bind<ITransformProvider>().To<TransformProvider>().FromInstance(_transformProvider).AsSingle();
            container.Bind<ISceneEntryCameraProvider>().To<SceneEntryCameraProvider>().FromInstance(_sceneEntryCameraProvider).AsSingle();
            
            container.Bind<IInit>().To<SceneEntryCameraSystem>().AsSingle();
        }
    }
}