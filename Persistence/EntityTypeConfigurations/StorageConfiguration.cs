using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineCellar.Domain;

namespace WineCellar.Persistence.EntityTypeConfigurations;

public class StorageConfiguration : IEntityTypeConfiguration<Storage>
{
    public void Configure(EntityTypeBuilder<Storage> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Name).IsUnique();
        builder.Property(e => e.Name).HasMaxLength(50);
        builder.HasIndex(e => e.Type);
        builder.Property(e => e.Type).HasMaxLength(50);
        builder.HasOne(e => e.Cellar).WithMany(e => e.Storages).HasForeignKey(e => e.CellarId);
        builder
            .HasMany(e => e.Wines)
            .WithOne(e => e.Storage)
            .HasForeignKey(e => e.StorageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
