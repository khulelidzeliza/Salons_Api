



using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Salons_Project.Domain.Models.Content;

namespace Salons_Project.Infrastructure.Persistence.Configurations;

public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(f => f.User)
            .WithMany()                             
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.NoAction);    

        builder.HasOne(f => f.Salon)
            .WithMany()                              
            .HasForeignKey(f => f.SalonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}