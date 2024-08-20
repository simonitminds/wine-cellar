using WineCellar.Domain;
using WineCellar.Persistence;

public class CreateCellar
{
    public class CreateCellarInput
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }

    public class CreateCellarOutput
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int Id { get; set; }
    }

    public async Task<CreateCellarOutput> ExecuteAsync(
        CreateCellarInput input,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var cellar = new Cellar { Name = input.Name, Description = input.Description };

        dbContext.Cellars.Add(cellar);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateCellarOutput
        {
            Name = cellar.Name,
            Description = cellar.Description,
            Id = cellar.Id,
        };
    }
}
