using _Project.Code.Core.Keys;
using UnityEngine;

namespace _Project.Code.Local
{
    public interface ITransformProvider
    {
        Transform GetTransform(TransformId name);
        bool TryGetTransform(TransformId name, out Transform transform);
    }
}