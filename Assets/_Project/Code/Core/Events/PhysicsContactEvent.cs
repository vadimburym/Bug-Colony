using _Project.Code.Core.Keys;
using UnityEngine;

namespace _Project.Code.Core.Events
{
    public readonly struct PhysicsContactEvent
    {
        public readonly EntityGUID Self;
        public readonly EntityGUID Collider;
        public readonly Vector2 NormalFromAToB;
        public readonly float Penetration;

        public PhysicsContactEvent(EntityGUID entityA, EntityGUID entityB, Vector2 normalFromAToB, float penetration)
        {
            Self = entityA;
            Collider = entityB;
            NormalFromAToB = normalFromAToB;
            Penetration = penetration;
        }
    }
}