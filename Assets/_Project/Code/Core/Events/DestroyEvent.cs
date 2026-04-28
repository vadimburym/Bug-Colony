using _Project.Code.Core.Keys;

namespace _Project.Code.Core.Events
{
    public struct DestroyEvent
    {
        public EntityGUID Entity;
        public DestroyCause Cause;
    }
}