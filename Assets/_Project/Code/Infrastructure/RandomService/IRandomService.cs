using UnityEngine;

namespace _Project.Code.Infrastructure
{
    public interface IRandomService
    {
        float Value01();
        int Range(int minInclusive, int maxExclusive);
        int Range(Vector2Int range);
        float Range(Vector2 range);
        Vector2 InsideRectangle(Vector2 halfExtents);
        Vector2 InsideRectangle(Vector2 min, Vector2 max);
    }
}