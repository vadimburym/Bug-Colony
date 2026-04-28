using _Project.Code.Core.Events;
using _Project.Code.Core.Keys;

namespace _Project.Code.Core.Abstractions
{
    public interface IConsumable
    {
        bool IsConsumed { get; }
        int ConsumeReward { get; }
        IConsumableRule ConsumableRule { get; }
        bool TryConsumeBy(EntityGUID consumer, out ConsumeEvent @event);
    }
}