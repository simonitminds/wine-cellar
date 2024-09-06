using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WineCellar.Domain;

namespace WineCellar.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "wineceller.db");
    }

    public string DbPath { get; }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Wine> Wines { get; set; } = null!;

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
    {
        OptionsBuilder.UseSqlite($"Data Source={DbPath}");
#if DEBUG
        OptionsBuilder
            .LogTo(
                message =>
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debug.WriteLine(message);
                    }
                },
                LogLevel.Information
            )
            .EnableSensitiveDataLogging();
#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
