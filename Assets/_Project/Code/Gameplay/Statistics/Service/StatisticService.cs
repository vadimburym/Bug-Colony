using System.Collections.Generic;
using _Project.Code.Core.Keys;
using UniRx;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public sealed class StatisticService : IStatisticService
    {
        private readonly Dictionary<BugId, ReactiveProperty<int>> _bugDeaths = new();

        public IReadOnlyReactiveProperty<int> GetBugDeathReactive(BugId bugId)
        {
            EvaluateBugDeath(bugId);
            return _bugDeaths[bugId];
        }

        public void RegisterDeath(BugId bugId)
        {
            EvaluateBugDeath(bugId);
            _bugDeaths[bugId].Value += 1;
        }
        
        private void EvaluateBugDeath(BugId bugId)
        {
            if (!_bugDeaths.ContainsKey(bugId))
                _bugDeaths.Add(bugId, new ReactiveProperty<int>(0));
        }
    }
}