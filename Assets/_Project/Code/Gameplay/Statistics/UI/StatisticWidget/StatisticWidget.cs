using System.Linq;
using _Project.Code.Local;
using UnityEngine;

namespace _Project.Code.Gameplay.Statistics.UI
{
    public sealed class StatisticWidget : MonoWidget<IStatisticPresenter>
    {
        [SerializeField] private DeathStatView _statPrefab;
        [SerializeField] private Transform _container;
        
        private UIGameObjectMemoryPool<DeathStatView> _pool;

        private void Awake()
        {
            _pool = new(
                _statPrefab,
                _container,
                _container.gameObject.GetComponentsInChildren<DeathStatView>().ToList());
        }
        
        public override void Initialize(IStatisticPresenter presenter)
        {
            for (int i = 0; i <  presenter.Presenters.Count; i++)
            {
                var itemPresenter =  presenter.Presenters[i];
                var view = _pool.SpawnItem();
                view.gameObject.SetActive(false);
                view.Initialize(itemPresenter);
                view.gameObject.SetActive(true);
            }
        }
    }
}