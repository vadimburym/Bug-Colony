using System;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Events;
using _Project.Code.Core.Keys;
using UnityEngine;

namespace _Project.Code.Gameplay.CoreFeatures.BugColony
{
    public sealed class BugEntity : 
        IConsumable,
        IHasLifeTime,
        IConsumer,
        ICirclePhysicsBody,
        IDestructible,
        IMovable
    {
        public EntityGUID GUID { get; private set; }
        public BugId BugId { get; private set; }
        
        public EntityGUID ConsumableTarget { get; private set; }
        public bool IsConsumed { get; private set; }
        public int ConsumeReward { get; private set; }
        public float LifeTime { get; private set; }
        public bool IsAlive { get; private set; }
        public float MoveSpeed { get; private set; }
        public Vector2 Velocity { get; private set; }
        public float ColliderRadius { get; private set; }
        public bool IsStaticBody { get; private set; }
        public IPhysicsContactRule[] ContactRules { get; private set; }
        public Vector2 WorldPosition { get; private set; }
        public IMovementRule MovementRule { get; }
        public int ConsumptionStorage { get; private set; }
        public IConsumableSelectorRule SelectorRule { get; private set; }
        public ISplitRule SplitRule { get; private set; }
        public IConsumableRule ConsumableRule { get; private set; }
        public ILifeTimeRule LifeTimeRule { get; private set; }
        public IDestroyRule DestroyRule { get; private set; }
        
        public BugEntity(
            EntityGUID guid,
            BugId bugId,
            float moveSpeed,
            int consumeReward,
            float colliderRadius,
            bool isStaticBody,
            IPhysicsContactRule[] contactRules,
            IConsumableSelectorRule selectorRule,
            ISplitRule splitRule,
            ILifeTimeRule lifeTimeRule,
            IDestroyRule destroyRule,
            IConsumableRule consumableRule,
            IMovementRule movementRule,
            Vector2 worldPosition)
        {
            GUID = guid;
            BugId = bugId;
            MoveSpeed = moveSpeed;
            ConsumeReward = consumeReward;
            SelectorRule = selectorRule;
            SplitRule = splitRule;
            LifeTimeRule = lifeTimeRule;
            DestroyRule = destroyRule;
            ConsumableRule = consumableRule;
            ColliderRadius = colliderRadius;
            IsStaticBody = isStaticBody;
            ContactRules = contactRules;
            MovementRule = movementRule;
            IsAlive = true;
            WorldPosition = worldPosition;
        }

        public void SetWorldPosition(Vector2 position) => WorldPosition = position;
        
        public void SetVelocity(Vector2 velocity) => Velocity = velocity;

        public void SetConsumableTarget(EntityGUID target) => ConsumableTarget = target;

        public bool TryUpdateLifetime(float deltaTime, out LifetimeUpdateEvent @event)
        {
            @event = default;
            if (!IsAlive)
                return false;
            LifeTime += deltaTime;
            @event = new LifetimeUpdateEvent { Entity = GUID, Lifetime = LifeTime };
            return true;
        }

        public bool TryIncreaseConsumption(int amount, out ConsumptionUpdateEvent @event)
        {
            @event = default;
            if (!IsAlive || amount <= 0)
                return false;
            ConsumptionStorage += amount;
            @event = new ConsumptionUpdateEvent { Entity = GUID, Consumption = ConsumptionStorage };
            return true;
        }

        public bool TryDecreaseConsumption(int amount, out ConsumptionUpdateEvent @event)
        {
            @event = default;
            if (!IsAlive || amount <= 0)
                return false;
            ConsumptionStorage = Math.Max(0, ConsumptionStorage - amount);
            @event = new ConsumptionUpdateEvent { Entity = GUID, Consumption = ConsumptionStorage };
            return true;
        }
        
        public bool TryResetConsumption(out ConsumptionUpdateEvent @event)
        {
            @event = default;
            if (!IsAlive)
                return false;
            ConsumptionStorage = 0;
            @event = new ConsumptionUpdateEvent { Entity = GUID, Consumption = ConsumptionStorage };
            return true;
        }

        public bool TryDestroy(DestroyCause cause, out DestroyEvent @event)
        {
            @event = default;
            if (!IsAlive)
                return false;
            IsAlive = false;
            @event = new DestroyEvent { Entity = GUID , Cause = cause };
            return true;
        }
        
        public bool TryConsumeBy(EntityGUID consumer, out ConsumeEvent @event)
        {
            @event = default;
            if (IsConsumed || ConsumeReward <= 0)
                return false;
            @event = new ConsumeEvent { Entity = GUID, ConsumeReward = ConsumeReward, Consumer = consumer };
            IsConsumed = true;
            return true;
        }
    }
}