using _Project.Code.Core.Keys;
using _Project.Code.Local;
using UnityEngine;

namespace _Project.Code.Gameplay._Core
{
    public sealed class EntityView : MonoBehaviour
    {
        public EntityGUID GUID { get; private set; }
        public MemoryPoolId PoolId { get; private set; }
        
        public void Bind(EntityGUID guid, MemoryPoolId poolId, Vector2 worldPosition)
        {
            GUID = guid;
            PoolId = poolId;
            UpdateWorldPosition(worldPosition);
        }

        public void Unbind()
        {
            GUID = EntityGUID.Null;
        }

        public void UpdateWorldPosition(Vector2 worldPosition)
        {
            transform.position = new Vector3(worldPosition.x, 0, worldPosition.y);
        }
    }
}