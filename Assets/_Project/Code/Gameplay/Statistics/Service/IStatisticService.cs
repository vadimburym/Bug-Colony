using _Project.Code.Core.Keys;
using UniRx;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public interface IStatisticService
    {
        IReadOnlyReactiveProperty<int> GetBugDeathReactive(BugId bugId);
        void RegisterDeath(BugId bugId);
    }
}