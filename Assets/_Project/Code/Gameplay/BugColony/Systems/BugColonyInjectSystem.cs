using _Project.Code.Core.Abstractions;
using _Project.Code.Infrastructure;
using _Project.Code.StaticData;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public sealed class BugColonyInjectSystem : IWarmUp
    {
        private readonly BugColonyStaticData _staticData;
        private readonly ILocalContextService _localContextService;
        
        public BugColonyInjectSystem(
            StaticDataService staticDataService,
            ILocalContextService localContextService)
        {
            _staticData = staticDataService.BugColonyStaticData;
            _localContextService = localContextService;
        }
        
        public void WarmUp()
        {
            foreach (var bug in _staticData.AllBugs)
            {
                _localContextService.Inject(bug.ConsumableRule);
                foreach (var rule in bug.ContactRules)
                    _localContextService.Inject(rule);
                _localContextService.Inject(bug.LifeTimeRule);
                _localContextService.Inject(bug.SelectorRule);
                _localContextService.Inject(bug.SplitRule);
                _localContextService.Inject(bug.DestroyRule);
                _localContextService.Inject(bug.MovementRule);
            }

            foreach (var food in _staticData.AllFoods)
            {
                _localContextService.Inject(food.ConsumableRule);
                _localContextService.Inject(food.DestroyRule);
                foreach (var rule in food.ContactRules)
                    _localContextService.Inject(rule);
            }
        }
    }
}