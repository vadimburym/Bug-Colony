using _Project.Code.Core.Abstractions;

namespace _Project.Code.Gameplay._Core.Movable
{
    public sealed class MoveSystem : IPausableTick
    {
        private readonly IMovableService _movableService;
        
        public MoveSystem(IMovableService movableService)
        {
            _movableService = movableService;
        }
        
        public void PausableTick()
        {
            _movableService.MoveAll();
        }
    }
}