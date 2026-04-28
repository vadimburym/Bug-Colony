using _Project.Code.Core.Keys;

namespace _Project.Code.Core.Abstractions
{
    public interface IConsumableSelectorRule
    {
        void Execute(IConsumer consumer, EntityGUID guid);
    }
}