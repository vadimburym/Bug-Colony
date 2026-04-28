using _Project.Code.StaticData;
using UniRx;
using UnityEngine;

namespace _Project.Code.Gameplay.Statistics.UI
{
    public sealed class DeathStatPresenter : IDeathStatPresenter
    {
        public Color IconColor => _config.UiIconColor;
        public IReadOnlyReactiveProperty<string> Deaths => _deaths;
        
        private readonly ReactiveProperty<string> _deaths = new(string.Empty);
        private readonly BugConfig _config;
        private readonly CompositeDisposable _disposables = new();
        
        public DeathStatPresenter(BugConfig config, IReadOnlyReactiveProperty<int> deaths)
        {
            _config = config;
            deaths.Subscribe(value => _deaths.Value = value.ToString()).AddTo(_disposables);
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}