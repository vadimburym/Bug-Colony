using _Project.Code.Core.Events;

namespace _Project.Code.Core.Abstractions
{
    public interface IHasLifeTime
    {
        float LifeTime { get; }
        ILifeTimeRule LifeTimeRule { get; }
        bool TryUpdateLifetime(float deltaTime, out LifetimeUpdateEvent @event);
    }
}