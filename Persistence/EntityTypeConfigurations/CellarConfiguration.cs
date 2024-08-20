using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineCellar.Domain;

namespace WineCellar.Persistence.EntityTypeConfigurations
{
    public class CellarConfiguration : IEntityTypeConfiguration<Cellar>
    {
        public void Configure(EntityTypeBuilder<Cellar> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.Description).HasMaxLength(500);
        }
    }
}
