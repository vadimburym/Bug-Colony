using UnityEngine;

namespace _Project.Code.Gameplay.BugColony.Providers
{
    public sealed class GroundAreaProvider : MonoBehaviour, IGroundAreaProvider
    {
        public Vector2 MinCorner => _minCornerPosition;
        public Vector2 MaxCorner => _maxCornerPosition;
        
        [SerializeField] private Transform _minCorner;
        [SerializeField] private Transform _maxCorner;
        
        private Vector2 _minCornerPosition;
        private Vector2 _maxCornerPosition;

        private void Awake()
        {
            _minCornerPosition = new Vector2(_minCorner.position.x, _minCorner.position.z);
            _maxCornerPosition = new Vector2(_maxCorner.position.x, _maxCorner.position.z);
        }
    }
}