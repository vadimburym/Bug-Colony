using UnityEngine;

namespace _Project.Code.Infrastructure
{
    public interface IMainCameraService
    {
        Camera MainCamera { get; }
        void SetupSceneEntry(Transform sceneEntry);
    }
}
