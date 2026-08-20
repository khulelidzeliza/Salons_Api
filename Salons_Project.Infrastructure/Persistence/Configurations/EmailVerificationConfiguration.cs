using System;
using System.Collections.Generic;
using System.Text;
using Salons_Project.Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Salons_Project.Infrastructure.Persistence.Configurations;

public class EmailVerificationConfiguration : IEntityTypeConfiguration<EmailVerification>
{
    public void Configure(EntityTypeBuilder<EmailVerification> builder)
    {
        builder.HasKey(x => x.Id);   // ← point this at the REAL key property

        // property constraints as needed, e.g.:
        // builder.Property(x => x.Code).IsRequired().HasMaxLength(6);
        // builder.Property(x => x.ExpiresAt).IsRequired();
    }
}
