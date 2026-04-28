using System.Threading;
using _Project.Code.Core.Keys;

namespace _Project.Code.Gameplay.CoreFeatures
{
    public sealed class EntityGuidService : IEntityGuidService
    {
        private long _current;

        public EntityGuidService(long startValue = 1)
        {
            _current = startValue;
        }

        public EntityGUID NewGuid()
        {
            var value = Interlocked.Increment(ref _current);
            return new EntityGUID(value);
        }
    }
}