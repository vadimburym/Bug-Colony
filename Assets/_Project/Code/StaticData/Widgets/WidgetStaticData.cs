using System.Collections.Generic;
using _Project.Code.Core.Keys;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace _Project.Code.StaticData
{
    [CreateAssetMenu(
        fileName = "WidgetStaticData",
        menuName = "_Project/Local/New WidgetStaticData"
    )]
    public sealed class WidgetStaticData : SerializedScriptableObject
    {
        [OdinSerialize] public Dictionary<GameStateId, ScreenId> MainScreens;
        [OdinSerialize] public Dictionary<ScreenId, WidgetId[]> ScreenData;
    }
}