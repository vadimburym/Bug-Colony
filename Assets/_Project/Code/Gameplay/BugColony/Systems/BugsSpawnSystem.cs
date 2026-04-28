using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;
using _Project.Code.Gameplay.BugColony.Providers;
using _Project.Code.Gameplay.CoreFeatures.BugColony;
using _Project.Code.Infrastructure;
using UnityEngine;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public sealed class BugsSpawnSystem : IPausableTick
    {
        private readonly IBugFactory _bugFactory;
        private readonly IBugColonyService _bugColonyService;
        private readonly IGroundAreaProvider _groundAreaProvider;
        private readonly IRandomService _randomService;
        
        public BugsSpawnSystem(
            IBugFactory bugFactory,
            IBugColonyService bugColonyService,
            IGroundAreaProvider groundAreaProvider,
            IRandomService randomService)
        {
            _bugFactory = bugFactory;
            _bugColonyService = bugColonyService;
            _groundAreaProvider = groundAreaProvider;
            _randomService = randomService;
        }
        
        public void PausableTick()
        {
            if (_bugColonyService.BugsCount > 0)
                return;
            var position = _randomService.InsideRectangle(
                _groundAreaProvider.MinCorner,
                _groundAreaProvider.MaxCorner);
            _bugFactory.CreateAndRegister(BugId.Worker, position);
        }
    }
}