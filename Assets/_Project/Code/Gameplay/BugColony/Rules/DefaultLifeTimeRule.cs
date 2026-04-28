using System;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Events;
using _Project.Code.Core.Keys;
using UnityEngine;
using Zenject;

namespace _Project.Code.Gameplay.CoreFeatures
{
    [Serializable]
    public sealed class DefaultLifeTimeRule : ILifeTimeRule
    {
        [SerializeField] private float _targetLifeTime;
        
        [Inject] private readonly IDestructibleService _destructibleService;
        
        public void Execute(LifetimeUpdateEvent @event)
        {
            if (@event.Lifetime < _targetLifeTime)
                return;
            
            if (_destructibleService.IsDestructible(@event.Entity))
                _destructibleService.Destroy(DestroyCause.LifeTime, @event.Entity);
        }
    }
}