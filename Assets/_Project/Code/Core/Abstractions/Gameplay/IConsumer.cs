using _Project.Code.Core.Events;
using _Project.Code.Core.Keys;

namespace _Project.Code.Core.Abstractions
{
    public interface IConsumer
    {
        int ConsumptionStorage { get; }
        EntityGUID ConsumableTarget { get; }
        IConsumableSelectorRule SelectorRule { get; }
        ISplitRule SplitRule { get; }
        bool TryIncreaseConsumption(int amount, out ConsumptionUpdateEvent @event);
        bool TryDecreaseConsumption(int amount, out ConsumptionUpdateEvent @event);
        bool TryResetConsumption(out ConsumptionUpdateEvent @event);
        void SetConsumableTarget(EntityGUID target);
    }
}