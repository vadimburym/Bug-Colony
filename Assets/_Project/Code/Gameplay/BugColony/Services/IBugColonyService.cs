using System.Collections.Generic;
using _Project.Code.Core.Keys;

namespace _Project.Code.Gameplay.CoreFeatures.BugColony
{
    public interface IBugColonyService
    {
        int BugsCount { get; }
        IReadOnlyCollection<EntityGUID> Foods { get; }
        IReadOnlyCollection<EntityGUID> Bugs { get; }
        void RegisterBug(BugEntity bug, EntityGUID guid);
        void RegisterFood(FoodEntity food, EntityGUID guid);
        IReadOnlyList<EntityGUID> GetAllBugsByType(BugId bugId);
        bool IsBug(EntityGUID guid, out BugId bugId);
        bool IsFood(EntityGUID guid);
        void UnregisterBug(EntityGUID guid);
        void UnregisterFood(EntityGUID guid);
    }
}