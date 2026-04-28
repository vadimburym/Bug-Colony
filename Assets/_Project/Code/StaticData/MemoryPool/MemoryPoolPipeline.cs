using UnityEngine;

namespace _Project.Code.StaticData
{
    [CreateAssetMenu(fileName = nameof(MemoryPoolPipeline), menuName ="_Project/Local/New MemoryPoolPipeline")]
    public sealed class MemoryPoolPipeline : ScriptableObject
    {
        public GameObjectMemoryPoolData[] GameObjectMemoryPools;
    }
}