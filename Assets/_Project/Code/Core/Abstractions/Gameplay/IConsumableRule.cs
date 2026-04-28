using _Project.Code.Core.Events;

namespace _Project.Code.Core.Abstractions
{
    public interface IConsumableRule
    {
        void Execute(ConsumeEvent @event);
    }
}