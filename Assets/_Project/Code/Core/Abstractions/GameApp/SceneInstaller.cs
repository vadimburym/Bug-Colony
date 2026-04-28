using UnityEngine;
using Zenject;

namespace _Project.Code.Core.Abstractions
{
    public abstract class SceneInstaller : MonoBehaviour
    {
        public abstract void Install(DiContainer container);
    }
}