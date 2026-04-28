using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public interface IDestructibleService
    {
        void Register(IDestructible entity, EntityGUID guid);
        bool IsDestructible(EntityGUID guid);
        void Destroy(DestroyCause cause, EntityGUID guid);
        void Unregister(EntityGUID guid);
    }
}