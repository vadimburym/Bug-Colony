using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Events;
using _Project.Code.Core.Keys;
using UnityEngine;

namespace _Project.Code.Gameplay.CoreFeatures.BugColony
{
    public sealed class FoodEntity :
        ICirclePhysicsBody,
        IConsumable,
        IDestructible
    {
        public EntityGUID GUID { get; private set; }
        public Vector2 WorldPosition { get; private set; }
        public Vector2 Velocity { get; private set; }
        public float ColliderRadius { get; private set; }
        public bool IsStaticBody { get; private set; }
        public IPhysicsContactRule[] ContactRules { get; private set; }
        public bool IsAlive { get; private set; }
        public bool IsConsumed { get; private set; }
        public int ConsumeReward { get; private set; }
        public IConsumableRule ConsumableRule { get; private set; }
        public IDestroyRule DestroyRule { get; private set; }

        public FoodEntity(
            EntityGUID guid,
            int consumeReward,
            IConsumableRule consumableRule,
            IDestroyRule destroyRule,
            IPhysicsContactRule[] contactRules,
            bool isStaticBody,
            float colliderRadius,
            Vector2 worldPosition)
        {
            GUID = guid;
            ConsumeReward = consumeReward;
            ConsumableRule = consumableRule;
            DestroyRule = destroyRule;
            IsAlive = true;
            ContactRules = contactRules;
            IsStaticBody = isStaticBody;
            ColliderRadius = colliderRadius;
            WorldPosition = worldPosition;
        }
        
        public void SetWorldPosition(Vector2 position) => WorldPosition = position;
        
        public void SetVelocity(Vector2 velocity) => Velocity = velocity;
        
        public bool TryConsumeBy(EntityGUID consumer, out ConsumeEvent @event)
        {
            @event = default;
            if (IsConsumed || ConsumeReward <= 0)
                return false;
            @event = new ConsumeEvent { Entity = GUID, ConsumeReward = ConsumeReward, Consumer = consumer };
            IsConsumed = true;
            return true;
        }
        
        public bool TryDestroy(DestroyCause cause, out DestroyEvent @event)
        {
            @event = default;
            if (!IsAlive)
                return false;
            IsAlive = false;
            @event = new DestroyEvent { Entity = GUID, Cause = cause };
            return true;
        }
    }
}