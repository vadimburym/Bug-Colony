using _Project.Code.Core.Keys;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Code.StaticData
{
    [CreateAssetMenu(fileName = nameof(WidgetConfig), menuName = "_Project/Local/New WidgetConfig")]
    public class WidgetConfig : ScriptableObject
    {
        public WidgetId WidgetId;
        public AssetReferenceGameObject WidgetReference;
        public bool ShowOnStart;
    }
}