using _Project.Code.Core.Keys;
using UnityEngine;

namespace _Project.Code.Core.Abstractions
{
    public interface ICirclePhysicsBody
    {
        EntityGUID GUID { get; }
        Vector2 WorldPosition { get; }
        Vector2 Velocity { get; }
        float ColliderRadius { get; }
        bool IsStaticBody { get; }
        IPhysicsContactRule[] ContactRules { get; }
        void SetWorldPosition(Vector2 position);
        void SetVelocity(Vector2 velocity);
    }
}