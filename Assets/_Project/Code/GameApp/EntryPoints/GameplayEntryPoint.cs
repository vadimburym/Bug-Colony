using System.Collections.Generic;
using _Project.Code.Core.Abstractions;
using _Project.Code.Infrastructure;
using _Project.Code.Local;
using Zenject;

namespace _Project.Code.GameApp
{
    public sealed class GameplayEntryPoint
    {
        private readonly IReadOnlyList<IWarmUp> _warmUp;
        private readonly IReadOnlyList<IInit> _init;
        private readonly IReadOnlyList<ITick> _tick;
        private readonly IReadOnlyList<IPausableTick> _pausableTick;
        private readonly IReadOnlyList<ILateTick> _lateTick;
        private readonly IReadOnlyList<IDispose> _dispose;
        private readonly IGamePauseService _gamePauseService;
        private readonly ISaveLoadService _saveLoadService;
        
        public GameplayEntryPoint(
            ISaveLoadService saveLoadService,
            IGamePauseService gamePauseService,
            [InjectLocal] List<IWarmUp> warmUp,
            [InjectLocal] List<IInit> init,
            [InjectLocal] List<ITick> tick,
            [InjectLocal] List<IPausableTick> pausableTick,
            [InjectLocal] List<ILateTick> lateTick,
            [InjectLocal] List<IDispose> dispose)
        {
            _saveLoadService = saveLoadService;
            _gamePauseService = gamePauseService;
            _warmUp = warmUp;
            _init = init;
            _tick = tick;
            _pausableTick = pausableTick;
            _lateTick = lateTick;
            _dispose = dispose;
        }

        public void Start()
        {
            for (int i = 0; i < _warmUp.Count; i++)
                _warmUp[i].WarmUp();
            for (int i = 0; i < _init.Count; i++)
                _init[i].Init();
            _saveLoadService.Load();
            _gamePauseService.SetGamePaused(false);
        }

        public void Tick()
        {
            for (int i = 0; i < _tick.Count; i++)
                _tick[i].Tick();
            if (!_gamePauseService.IsGamePaused.Value)
            {
                for (int i = 0; i < _pausableTick.Count; i++)
                    _pausableTick[i].PausableTick();
            }
        }

        public void LateTick()
        {
            for (int i = 0; i < _lateTick.Count; i++)
                _lateTick[i].LateTick();
        }
        
        public void Dispose()
        {
            for (int i = 0; i < _dispose.Count; i++)
                _dispose[i].Dispose();
        }
    }
}