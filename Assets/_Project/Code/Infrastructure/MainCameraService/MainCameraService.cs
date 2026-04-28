using UnityEngine;

namespace _Project.Code.Infrastructure
{
    public sealed class MainCameraService : IMainCameraService
    {
        public Camera MainCamera => _mainCamera;
        private readonly Camera _mainCamera = Camera.main;

        public void SetupSceneEntry(Transform sceneEntry)
        {
            _mainCamera.transform.position = sceneEntry.position;
            _mainCamera.transform.rotation = sceneEntry.rotation;
        }
    }
}
