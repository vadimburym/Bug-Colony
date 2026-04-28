using _Project.Code.Core.Keys;

namespace _Project.Code.Core.Abstractions
{
    public interface IGameState
    {
        GameStateId GameStateId { get; }
        void Enter();
        void Tick();
        void LateTick();
        void Dispose();
    }
}