using UnityEngine;

namespace _Project.Code.Gameplay.BugColony.Providers
{
    public interface IGroundAreaProvider
    {
        Vector2 MinCorner { get; }
        Vector2 MaxCorner { get; }
    }
}