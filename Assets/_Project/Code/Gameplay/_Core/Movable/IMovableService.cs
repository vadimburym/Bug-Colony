using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;

namespace _Project.Code.Gameplay._Core.Movable
{
    public interface IMovableService
    {
        void Register(IMovable movable);
        void Unregister(EntityGUID guid);
        void MoveAll();
    }
}