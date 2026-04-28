using _Project.Code.Core.Events;

namespace _Project.Code.Core.Abstractions
{
    public interface ILifeTimeRule
    {
        void Execute(LifetimeUpdateEvent @event);
    }
}