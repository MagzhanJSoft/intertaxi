using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using IntercityTaxi.Domain.Models;

namespace IntercityTaxi.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedNever();

        builder.Property(u => u.PhoneNumber)
           .IsRequired()
           .HasMaxLength(11);

        builder.Property(u => u.Password)
           .IsRequired()
           .HasMaxLength(100);

        builder.Property(u => u.RefreshToken)
           .HasMaxLength(256);

        builder.Property(u => u.FullName)
            .HasMaxLength(100);

        // Настройка связи с UserRole, если необходимо
        builder.HasOne(u => u.Role)
           .WithMany() // если у UserRole нет коллекции пользователей, это правильно
           .HasForeignKey(u=>u.RoleId); // для связи с таблицей UserRoles
    }
}
