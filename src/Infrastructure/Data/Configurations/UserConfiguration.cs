using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Usuario");
            
        builder.HasKey(e => e.Id);
            
        builder.Property(e => e.Id)
            .HasColumnName("IdUsuario");

        builder.Property(e => e.LastName)
            .HasColumnName("Apellidos")
            .HasMaxLength(50)
            .IsUnicode(false);
            
        builder.Property(e => e.Email)
            .HasMaxLength(30)
            .IsUnicode(false);
            
        builder.Property(e => e.DateOfBirth)
            .HasColumnName("FechaNacimiento")
            .HasColumnType("date");
            
        builder.Property(e => e.FirstName)
            .HasColumnName("Nombres")
            .HasMaxLength(50)
            .IsUnicode(false);
            
        builder.Property(e => e.Telephone)
            .HasColumnName("Telefono")
            .HasMaxLength(10)
            .IsUnicode(false);
            
        builder.Property(e => e.IsActive)
            .HasColumnName("Activo");
    }
}