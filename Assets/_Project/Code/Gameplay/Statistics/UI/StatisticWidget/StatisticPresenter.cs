using System.Collections.Generic;

namespace _Project.Code.Gameplay.Statistics.UI
{
    public sealed class StatisticPresenter : IStatisticPresenter
    {
        public IReadOnlyList<IDeathStatPresenter> Presenters => _presenters;
        private readonly List<IDeathStatPresenter> _presenters;

        public StatisticPresenter(List<IDeathStatPresenter> presenters)
        {
            _presenters = presenters;
        }
        
        public void Dispose()
        {
            for (int i = 0; i < _presenters.Count; i++)
                _presenters[i].Dispose();
        }
    }
}