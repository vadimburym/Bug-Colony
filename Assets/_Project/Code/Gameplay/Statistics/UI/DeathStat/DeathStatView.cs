using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Gameplay.Statistics.UI
{
    public sealed class DeathStatView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _deathsText;
        
        private readonly CompositeDisposable _disposables = new();
        private IDeathStatPresenter _presenter;
        
        public void Initialize(IDeathStatPresenter presenter)
        {
            _presenter = presenter;
            _icon.color = _presenter.IconColor;
        }

        private void OnEnable()
        {
            if (_presenter == null)
                return;
            _presenter.Deaths.Subscribe(value => _deathsText.text = value).AddTo(_disposables);
        }

        private void OnDisable()
        {
            if (_presenter == null)
                return;
            _disposables.Clear();
        }
    }
}