using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;
using UnityEngine;

namespace _Project.Code.StaticData
{
    [CreateAssetMenu(fileName = nameof(BugConfig), menuName = "_Project/Gameplay/New Bug")]
    public sealed class BugConfig : ScriptableObject
    {
        public Color UiIconColor;
        public int ConsumeReward;
        public float MoveSpeed;
        public float ColliderRadius;
        public bool IsStaticBody;
        public MemoryPoolId ViewPoolId; 
        
        [SerializeReference] public IPhysicsContactRule[] ContactRules;
        [SerializeReference] public IConsumableSelectorRule SelectorRule;
        [SerializeReference] public ISplitRule SplitRule;
        [SerializeReference] public IConsumableRule ConsumableRule;
        [SerializeReference] public ILifeTimeRule LifeTimeRule;
        [SerializeReference] public IDestroyRule DestroyRule;
        [SerializeReference] public IMovementRule MovementRule;
    }
}