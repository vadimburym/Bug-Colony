using System;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Events;

namespace _Project.Code.Gameplay.CoreFeatures
{
    [Serializable]
    public sealed class InfinityLifeTimeRule : ILifeTimeRule
    {
        public void Execute(LifetimeUpdateEvent @event) { return; }
    }
}