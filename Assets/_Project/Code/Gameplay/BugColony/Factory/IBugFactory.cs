using _Project.Code.Core.Keys;
using UnityEngine;

namespace _Project.Code.Gameplay.CoreFeatures.BugColony
{
    public interface IBugFactory
    {
        BugEntity CreateAndRegister(BugId bugId, Vector2 worldPosition);
    }
}