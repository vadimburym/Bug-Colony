using System.Collections.Generic;
using _Project.Code.Gameplay.CoreFeatures;
using _Project.Code.Local;
using _Project.Code.StaticData;

namespace _Project.Code.Gameplay.Statistics.UI
{
    public sealed class StatisticWidgetShower : WidgetShower<IStatisticPresenter, StatisticWidget>
    {
        private readonly BugColonyStaticData _staticData;
        private readonly IStatisticService _statisticService;
        private readonly StatisticWidgetConfig _config;
        
        public StatisticWidgetShower(
            StaticDataService staticDataService,
            IStatisticService statisticService,
            StatisticWidgetConfig config) : base(config)
        {
            _staticData = staticDataService.BugColonyStaticData;
            _statisticService = statisticService;
            _config = config;
        }

        protected override IStatisticPresenter CreatePresenter()
        {
            var presenters = new List<IDeathStatPresenter>();
            var bugsToShow = _config.BugsToShow;
            for (int i = 0; i < bugsToShow.Count; i++)
            {
                var bugId = bugsToShow[i];
                var reactive = _statisticService.GetBugDeathReactive(bugId);
                var config = _staticData.GetBugConfig(bugId);
                presenters.Add(new DeathStatPresenter(config, reactive));
            }

            return new StatisticPresenter(presenters);
        }
    }
}