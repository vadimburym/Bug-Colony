namespace _Project.Code.Core.Abstractions
{
    public interface ISaveDataSetup<TSaveData>
    {
        void SetupSaveData(TSaveData saveData);
    }
}