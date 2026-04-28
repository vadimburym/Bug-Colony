using _Project.Code.Core.Abstractions;
using _Project.Code.Infrastructure;

namespace _Project.Code.Local
{
    public sealed class SceneEntryCameraSystem : IInit
    {
        private readonly ISceneEntryCameraProvider _sceneEntryCameraProvider;
        private readonly IMainCameraService _mainCameraService;
        
        public SceneEntryCameraSystem(
            ISceneEntryCameraProvider sceneEntryCameraProvider,
            IMainCameraService mainCameraService)
        {
            _sceneEntryCameraProvider = sceneEntryCameraProvider;
            _mainCameraService = mainCameraService;
        }
        
        public void Init()
        {
            _mainCameraService.SetupSceneEntry(_sceneEntryCameraProvider.GetEntryReference());
            _sceneEntryCameraProvider.DisposeReference();
        }
    }
}