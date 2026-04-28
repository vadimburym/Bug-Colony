using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Keys;
using _Project.Code.Gameplay.BugColony.Providers;
using _Project.Code.Gameplay.CoreFeatures.BugColony;
using _Project.Code.Infrastructure;
using _Project.Code.StaticData;
using UnityEngine;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public sealed class FoodSpawnSystem : IPausableTick
    {
        private readonly IRandomService _randomService;
        private readonly BugColonyStaticData _staticData;
        private readonly IFoodFactory _foodFactory;
        private readonly IGroundAreaProvider _groundAreaProvider;
        
        private float _time;
        
        public FoodSpawnSystem(
            IRandomService randomService,
            StaticDataService staticDataService,
            IFoodFactory foodFactory,
            IGroundAreaProvider groundAreaProvider)
        {
            _staticData = staticDataService.BugColonyStaticData;
            _randomService = randomService;
            _foodFactory = foodFactory;
            _groundAreaProvider = groundAreaProvider;
        }
        
        public void PausableTick()
        {
            _time -= Time.deltaTime;
            
            if (_time > 0)
                return;

            var count = _randomService.Range(_staticData.FoodSpawnCount);
            for (int i = 0; i < count; i++)
            {
                var position = _randomService.InsideRectangle(
                    _groundAreaProvider.MinCorner,
                    _groundAreaProvider.MaxCorner);
                _foodFactory.CreateAndRegister(FoodId.Default, position);
            }
            
            _time += _randomService.Range(_staticData.FoodSpawnInterval);
        }
    }
}