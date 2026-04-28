using UnityEngine;

namespace _Project.Code.Local
{
    public interface ISceneEntryCameraProvider
    {
        Transform GetEntryReference();
        void DisposeReference();
    }
}