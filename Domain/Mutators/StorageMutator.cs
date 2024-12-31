using WineCellar.Domain;
using WineCellar.Feature.Storages;

public static class StorageMutator
{
    public static void MutateStorage(StorageRequest request, Storage storage)
    {
        storage.Name = request.Name;
        storage.Type = request.Type;
        storage.Temperature = request.Temperature;
        storage.Capacity = request.Capacity;
    }
}
