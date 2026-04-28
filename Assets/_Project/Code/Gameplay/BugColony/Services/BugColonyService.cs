using System.Collections.Generic;
using _Project.Code.Core.Keys;

namespace _Project.Code.Gameplay.CoreFeatures.BugColony
{
    public sealed class BugColonyService : IBugColonyService
    {
        public int BugsCount => _bugsByGuid.Count;
        public IReadOnlyCollection<EntityGUID> Foods => _foodsByGuid.Keys;
        public IReadOnlyCollection<EntityGUID> Bugs => _bugsByGuid.Keys;
        
        private readonly Dictionary<EntityGUID, BugEntity> _bugsByGuid = new();
        private readonly Dictionary<BugId, List<EntityGUID>> _bugsByType = new();
        private readonly Dictionary<EntityGUID, FoodEntity> _foodsByGuid = new();
        
        public bool IsFood(EntityGUID guid) => _foodsByGuid.ContainsKey(guid);
        
        public bool IsBug(EntityGUID guid, out BugId bugId)
        {
            if (_bugsByGuid.TryGetValue(guid, out var bug))
            {
                bugId = bug.BugId;
                return true;
            }
            bugId = default;
            return false;
        }
        
        public IReadOnlyList<EntityGUID> GetAllBugsByType(BugId bugId) 
            => _bugsByType[bugId];
        
        public void RegisterBug(BugEntity bug, EntityGUID guid)
        {
            _bugsByGuid.Add(guid, bug);
            if (!_bugsByType.ContainsKey(bug.BugId))
                _bugsByType.Add(bug.BugId, new List<EntityGUID>());
            _bugsByType[bug.BugId].Add(bug.GUID);
        }
        
        public void RegisterFood(FoodEntity food, EntityGUID guid)
        {
            _foodsByGuid.Add(guid, food);
        }
        
        public void UnregisterBug(EntityGUID guid)
        {
            var bug = _bugsByGuid[guid];
            _bugsByType[bug.BugId].Remove(bug.GUID);
            _bugsByGuid.Remove(guid);
        }
        
        public void UnregisterFood(EntityGUID guid)
        {
            _foodsByGuid.Remove(guid);
        }
    }
}