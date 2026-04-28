namespace _Project.Code.Core.Abstractions
{
    public interface ISaveDataConvert<TSaveData>
    {
        TSaveData ConvertToSaveData();
    }
}