using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public interface IConsumableService
    {
        void Register(IConsumable consumable, EntityGUID guid);
        bool IsConsumable(EntityGUID guid);
        void Consume(EntityGUID consumerId, EntityGUID consumableId);
        bool IsConsumed(EntityGUID guid);
        void Unregister(EntityGUID guid);
    }
}