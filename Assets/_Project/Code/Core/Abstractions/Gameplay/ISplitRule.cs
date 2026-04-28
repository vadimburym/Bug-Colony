using _Project.Code.Core.Events;

namespace _Project.Code.Core.Abstractions
{
    public interface ISplitRule
    {
        void Execute(ConsumptionUpdateEvent @event);
    }
}