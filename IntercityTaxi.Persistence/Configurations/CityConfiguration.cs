using IntercityTaxi.Domain.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntercityTaxi.Persistence.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
           .IsRequired()
           .HasMaxLength(50);
    }
}
