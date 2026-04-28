using _Project.Code.Core.Keys;
using UnityEngine;

namespace _Project.Code.Gameplay.CoreFeatures.BugColony
{
    public interface IFoodFactory
    {
        FoodEntity CreateAndRegister(FoodId foodId, Vector2 position);
    }
}