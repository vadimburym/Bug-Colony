using UnityEngine;

namespace _Project.Code.Infrastructure
{
    public sealed class LoadingCurtainProvider : MonoBehaviour, ILoadingCurtainProvider
    {
        [SerializeField] private GameObject _root;
        
        public void Show() => _root.SetActive(true);
        public void Hide() => _root.SetActive(false);
    }
}