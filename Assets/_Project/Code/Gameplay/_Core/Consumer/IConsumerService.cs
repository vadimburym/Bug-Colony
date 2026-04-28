using System.Collections.Generic;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public interface IConsumerService
    {
        IReadOnlyCollection<EntityGUID> Consumers { get; }
        void Register(IConsumer consumer, EntityGUID guid);
        bool IsConsumer(EntityGUID guid);
        int GetConsumptionStorage(EntityGUID guid);
        void SelectConsumableTarget(EntityGUID guid);
        EntityGUID GetConsumableTarget(EntityGUID guid);
        void IncreaseConsumption(EntityGUID guid, int amount);
        void DecreaseConsumption(EntityGUID guid, int amount);
        void ResetConsumption(EntityGUID guid);
        void Unregister(EntityGUID guid);
    }
}