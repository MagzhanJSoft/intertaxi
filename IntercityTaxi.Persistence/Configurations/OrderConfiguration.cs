using IntercityTaxi.Domain.Models;
using IntercityTaxi.Domain.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntercityTaxi.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .IsRequired()
            .ValueGeneratedNever(); // Guid генерируется в коде, а не БД

        builder.Property(o => o.CreatedById)
            .IsRequired();

        builder.Property(o => o.FromCityId)
            .IsRequired();

        builder.Property(o => o.FromAddress)
            .HasMaxLength(255);

        builder.Property(o => o.ToCityId)
            .IsRequired();       

        builder.Property(o => o.ToAddress)
            .HasMaxLength(255);

        builder.Property(o => o.Date)
            .IsRequired();

        builder.Property(o => o.TripTypeId)
            .IsRequired();

        builder.Property(o => o.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)"); // Храним цену корректно

        builder.Property(o => o.Comment)
            .HasMaxLength(500);

        builder.Property(o => o.CreatedByRoleId)
            .IsRequired();

        builder.Property(o => o.CreatedAt)
            .IsRequired();

        builder.Property(o => o.ExpirationDate)
            .IsRequired();       

        builder.HasOne(o => o.CreatedBy)
            .WithMany() // Один пользователь может создавать много заказов
            .HasForeignKey(o => o.CreatedById)
            .OnDelete(DeleteBehavior.Restrict); // Не удаляем заказ при удалении пользователя

        builder.HasOne(o => o.FromCity)
            .WithMany()
            .HasForeignKey(o => o.FromCityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.ToCity)
            .WithMany()
            .HasForeignKey(o => o.ToCityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.TripType)
            .WithMany()
            .HasForeignKey(o => o.TripTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.CreatedByRole)
            .WithMany()
            .HasForeignKey(o => o.CreatedByRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Orders");
    }
}

