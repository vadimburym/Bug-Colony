using _Project.Code.Core.Events;
using _Project.Code.Core.Keys;

namespace _Project.Code.Core.Abstractions
{
    public interface IDestructible
    {
        bool IsAlive { get; }
        IDestroyRule DestroyRule { get; }
        bool TryDestroy(DestroyCause cause, out DestroyEvent @event);
    }
}