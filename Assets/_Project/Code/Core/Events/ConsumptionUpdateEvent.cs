using _Project.Code.Core.Keys;

namespace _Project.Code.Core.Events
{
    public struct ConsumptionUpdateEvent
    {
        public EntityGUID Entity; 
        public int Consumption;
    }
}