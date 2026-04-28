using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;
using UnityEngine;

namespace _Project.Code.StaticData
{
    [CreateAssetMenu(fileName = nameof(FoodConfig), menuName = "_Project/Gameplay/New Food")]
    public sealed class FoodConfig : ScriptableObject
    {
        public int ConsumeReward;
        public bool IsStaticBody;
        public float ColliderRadius;
        public MemoryPoolId ViewPoolId;
        
        [SerializeReference] public IPhysicsContactRule[] ContactRules;
        [SerializeReference] public IConsumableRule ConsumableRule;
        [SerializeReference] public IDestroyRule DestroyRule;
    }
}