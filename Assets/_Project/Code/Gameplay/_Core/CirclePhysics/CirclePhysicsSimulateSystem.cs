using _Project.Code.Core.Abstractions;
using _Project.Code.Gameplay.BugColony.Providers;
using _Project.Code.StaticData;
using UnityEngine;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public sealed class CirclePhysicsSimulateSystem : IPausableTick
    {
        private readonly ICirclePhysicsService _physicsService;
        private readonly IGroundAreaProvider _groundAreaProvider;
        
        public CirclePhysicsSimulateSystem(
            ICirclePhysicsService physicsService,
            IGroundAreaProvider groundAreaProvider)
        {
            _physicsService = physicsService;
            _groundAreaProvider = groundAreaProvider;
        }

        public void PausableTick()
        {
            _physicsService.Simulate(Time.deltaTime, _groundAreaProvider.MinCorner, _groundAreaProvider.MaxCorner);
        }
    }
}