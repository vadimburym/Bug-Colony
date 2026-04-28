namespace _Project.Code.Core.Abstractions
{
    public interface IMovementRule
    {
        void Execute(IMovable movable);
    }
}