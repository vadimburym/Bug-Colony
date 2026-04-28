using _Project.Code.Core.Keys;

namespace _Project.Code.Core.Events
{
    public struct ConsumeEvent
    {
        public EntityGUID Entity;
        public int ConsumeReward;
        public EntityGUID Consumer;
    }
}