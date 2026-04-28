using _Project.Code.Core.Keys;

namespace _Project.Code.Infrastructure
{
    public interface IStateMachine
    {
        GameStateId CurrentStateId { get; }
        void Enter(GameStateId gameStateId);
        void Tick();
        void LateTick();
        void Dispose();
    }
}