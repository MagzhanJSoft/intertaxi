using IntercityTaxi.Domain.Models.Order;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IntercityTaxi.Persistence.Configurations;

public class TripTypeConfiguration : IEntityTypeConfiguration<TripType>
{
    public void Configure(EntityTypeBuilder<TripType> builder)
    {
        builder.ToTable("TripTypes");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
           .IsRequired()
           .HasMaxLength(50);
    }
}
