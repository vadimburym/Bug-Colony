using _Project.Code.Core.Abstractions;
using UnityEngine;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public sealed class LifeTimeUpdateSystem : IPausableTick
    {
        private readonly ILifeTimeService _lifeTimeService;
        
        public LifeTimeUpdateSystem(ILifeTimeService lifeTimeService)
        {
            _lifeTimeService = lifeTimeService;
        }
        
        public void PausableTick()
        {
            _lifeTimeService.UpdateAllLifeTime(Time.deltaTime);
        }
    }
}