using _Project.Code.Core.Keys;
using UnityEngine;

namespace _Project.Code.Core.Abstractions
{
    public interface IMovable
    {
        EntityGUID GUID { get; }
        float MoveSpeed { get; }
        Vector2 Velocity { get; }
        Vector2 WorldPosition { get; }
        IMovementRule MovementRule { get; }
        void SetVelocity(Vector2 velocity);
    }
}