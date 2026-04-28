using System.Collections;
using System.Collections.Generic;
using _Project.Code.Core.Keys;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace _Project.Code.StaticData
{
    [CreateAssetMenu(fileName = nameof(BugColonyStaticData), menuName = "_Project/Gameplay/New BugColonyStaticData")]
    public sealed class BugColonyStaticData : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<BugId, BugConfig> _bugs;
        [OdinSerialize] private Dictionary<FoodId, FoodConfig> _foods;

        public Vector2 FoodSpawnInterval;
        public Vector2Int FoodSpawnCount;
        
        public BugConfig GetBugConfig(BugId bugId) => _bugs[bugId];
        public FoodConfig GetFoodConfig(FoodId foodId) => _foods[foodId];
        
        public IReadOnlyCollection<BugConfig> AllBugs => _bugs.Values;
        public IReadOnlyCollection<FoodConfig> AllFoods => _foods.Values;
    }
}