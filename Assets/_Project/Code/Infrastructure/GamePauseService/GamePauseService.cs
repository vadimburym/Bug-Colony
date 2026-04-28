using UniRx;

namespace _Project.Code.Infrastructure
{
    public sealed class GamePauseService : IGamePauseService
    {
        public IReadOnlyReactiveProperty<bool> IsGamePaused => _isGamePaused;
        private readonly ReactiveProperty<bool> _isGamePaused = new();
        
        public void SetGamePaused(bool value) => _isGamePaused.Value = value;
    }
}