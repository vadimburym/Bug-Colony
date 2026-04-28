using System.Collections.Generic;
using _Project.Code.Core.Keys;
using UnityEngine;

namespace _Project.Code.StaticData
{
    [CreateAssetMenu(fileName = nameof(WidgetConfig), menuName = "_Project/Local/New StatisticWidgetConfig")]
    public class StatisticWidgetConfig : WidgetConfig
    {
        public List<BugId> BugsToShow;
    }
}