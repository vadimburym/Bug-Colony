using UniRx;

namespace _Project.Code.Infrastructure
{
    public interface IGamePauseService
    {
        IReadOnlyReactiveProperty<bool> IsGamePaused { get; }
        void SetGamePaused(bool value);
    }
}