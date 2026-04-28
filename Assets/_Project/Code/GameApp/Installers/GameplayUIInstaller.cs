using _Project.Code.Gameplay.Statistics.UI;
using _Project.Code.StaticData;
using UnityEngine;
using Zenject;

namespace _Project.Code.GameApp
{
    [CreateAssetMenu(fileName = "GameplayUIInstaller", menuName = "_Project/UI Installers/New GameplayUIInstaller")]
    public sealed class GameplayUIInstaller : ScriptableObject
    {
        [SerializeField] private StatisticWidgetConfig _bugHudConfig;

        public void Install(DiContainer container)
        {
            container.BindInterfacesTo<StatisticWidgetShower>().AsSingle().WithArguments(_bugHudConfig);
        }
    }
}