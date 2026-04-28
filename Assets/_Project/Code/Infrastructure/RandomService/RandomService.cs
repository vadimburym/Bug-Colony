using UnityEngine;

namespace _Project.Code.Infrastructure
{
    public sealed class RandomService : IRandomService
    {
        public float Value01() => Random.value;
        
        public int Range(int minInclusive, int maxExclusive) => Random.Range(minInclusive, maxExclusive);
        
        public int Range(Vector2Int range) => Random.Range(range.x, range.y);
        
        public float Range(Vector2 range) => Random.Range(range.x, range.y);
        
        public Vector2 InsideRectangle(Vector2 halfExtents)
        {
            return new Vector2(
                Random.Range(-halfExtents.x, halfExtents.x),
                Random.Range(-halfExtents.y, halfExtents.y));
        }

        public Vector2 InsideRectangle(Vector2 min, Vector2 max)
        {
            return new Vector2(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y));
        }
    }
}