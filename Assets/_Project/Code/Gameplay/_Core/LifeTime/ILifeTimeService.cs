using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public interface ILifeTimeService
    {
        void Register(IHasLifeTime entity, EntityGUID guid);
        bool HasLifeTime(EntityGUID guid);
        void UpdateLifeTime(EntityGUID guid, float deltaTime);
        void UpdateAllLifeTime(float deltaTime);
        void Unregister(EntityGUID guid);
    }
}