using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineCellar.Domain;

namespace WineCellar.Persistence.EntityTypeConfigurations;

public class WineConfiguration : IEntityTypeConfiguration<Wine>
{
    public void Configure(EntityTypeBuilder<Wine> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Name);
        builder.Property(e => e.Name).HasMaxLength(50);
        builder.HasIndex(e => e.Type);
        builder.Property(e => e.Type).HasMaxLength(50);
        builder.Property(e => e.Description).HasMaxLength(250);
        builder.HasIndex(e => e.ExpirationTime);
        builder.HasOne(e => e.Storage).WithMany(e => e.Wines).HasForeignKey(e => e.StorageId);
    }
}
