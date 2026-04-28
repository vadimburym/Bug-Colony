using UnityEngine;

namespace _Project.Code.GameApp
{
    [CreateAssetMenu(fileName = "UIInstallers", menuName = "_Project/New UIInstallers")]
    public sealed class GameUIInstallers : ScriptableObject
    {
        public GameplayUIInstaller GameplayUIInstaller;
    }
}