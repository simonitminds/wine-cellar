using WineCellar.Domain;
using WineCellar.Feature.Wines;

public static class WineMutator
{
    public static void Mutatewine(WineRequest request, Wine wine)
    {
        wine.Name = request.Name;
        wine.Year = request.Year;
        wine.Type = request.Type;
        wine.Quantity = request.Quantity;
        wine.Description = request.Description;
    }
}
