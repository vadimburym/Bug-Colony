using _Project.Code.Core.Events;

namespace _Project.Code.Core.Abstractions
{
    public interface IDestroyRule
    {
        void Execute(DestroyEvent @event);
    }
}