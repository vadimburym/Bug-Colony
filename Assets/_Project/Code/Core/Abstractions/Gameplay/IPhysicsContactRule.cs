using _Project.Code.Core.Events;

namespace _Project.Code.Core.Abstractions
{
    public interface IPhysicsContactRule
    {
        void Execute(PhysicsContactEvent @event);
    }
}