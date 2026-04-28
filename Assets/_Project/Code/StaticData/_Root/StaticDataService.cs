using UnityEngine;

namespace _Project.Code.StaticData
{
    [CreateAssetMenu(fileName = "StaticDataService", menuName = "_Project/New StaticDataService")]
    public sealed class StaticDataService : ScriptableObject
    {
        public MemoryPoolPipeline MemoryPoolPipeline;
        public WidgetStaticData WidgetStaticData;
        public BugColonyStaticData BugColonyStaticData;
    }
}