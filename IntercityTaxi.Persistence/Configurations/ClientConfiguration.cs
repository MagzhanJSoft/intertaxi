using IntercityTaxi.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IntercityTaxi.Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");
        builder.HasBaseType<User>();

        builder.Property(c => c.Rating)
            .IsRequired()
            .HasDefaultValue(3.0f);

        builder.HasBaseType<User>();

    }
}
