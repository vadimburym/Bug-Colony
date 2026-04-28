using System.Collections.Generic;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Events;
using _Project.Code.Core.Keys;
using UnityEngine;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public interface ICirclePhysicsService
    {
        IReadOnlyList<PhysicsContactEvent> Contacts { get; }
        void Register(ICirclePhysicsBody body);
        bool TryGetBody(EntityGUID entityGUID, out ICirclePhysicsBody body);
        void Unregister(EntityGUID guid);
        void Simulate(float deltaTime, Vector2 minCorner, Vector2 maxCorner);
    }
}