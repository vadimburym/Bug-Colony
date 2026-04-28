using System;
using _Project.Code.Core.Keys;
using UnityEngine.AddressableAssets;

namespace _Project.Code.StaticData
{
    [Serializable]
    public struct GameObjectMemoryPoolData
    {
        public MemoryPoolId PoolId;
        public TransformId TransformId;
        public AssetReferenceGameObject AssetReference;
        public int InitialCount;
    }
}