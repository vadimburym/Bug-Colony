using _Project.Code.Core.Keys;

namespace _Project.Code.Core.Events
{
    public struct LifetimeUpdateEvent
    {
        public EntityGUID Entity; 
        public float Lifetime;
    }
}