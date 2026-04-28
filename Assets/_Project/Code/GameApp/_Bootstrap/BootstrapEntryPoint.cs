using System;
using _Project.Code.Core.Keys;
using _Project.Code.Infrastructure;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Code.GameApp
{
    public sealed class BootstrapEntryPoint : IInitializable, ITickable, ILateTickable, IDisposable
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISaveRepository _saveRepository;
        
        public BootstrapEntryPoint(
            IStateMachine stateMachine,
            ISaveRepository saveRepository)
        {
            _stateMachine = stateMachine;
            _saveRepository = saveRepository;
        }
        
        public void Initialize() => InitializeAsync().Forget();
        
        private async UniTask InitializeAsync()
        {
            await _saveRepository.Load();
            
            //connect with server
            //warmup SDK
            //and e.t.c.
            
            //choose Tutorial if it not complete
            _stateMachine.Enter(GameStateId.Gameplay);
        }

        public void Tick() => _stateMachine.Tick();
        
        public void LateTick() => _stateMachine.LateTick();
        
        public void Dispose() => _stateMachine.Dispose();
    }
}