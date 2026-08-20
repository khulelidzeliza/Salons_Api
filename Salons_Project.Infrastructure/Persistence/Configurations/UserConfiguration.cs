



using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Salons_Project.Domain.Models.Auth;
using Salons_Project.Domain.Models.Users;

namespace Salons_Project.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        // ✅ Configure Email Value Object
        builder.ComplexProperty(x => x.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(255);
        });

        // ✅ Configure properties
        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(x => x.Password)
            .HasMaxLength(255);

        builder.Property(x => x.Role)
            .HasConversion<string>();

        // ✅ Configure one-to-one relationships with NO CASCADE DELETE
        builder.HasOne(u => u.EmailVerification)
            .WithOne(e => e.User)
            .HasForeignKey<EmailVerification>(e => e.UserId)
            .OnDelete(DeleteBehavior.NoAction);  // ← CRITICAL!

        builder.HasOne(u => u.PasswordEmailVerification)
            .WithOne(p => p.User)
            .HasForeignKey<PasswordVerification>(p => p.UserId)
            .OnDelete(DeleteBehavior.NoAction);  // ← CRITICAL!
    }
}
