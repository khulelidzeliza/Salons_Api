
using Microsoft.EntityFrameworkCore;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Domain.Models.Auth;
using Salons_Project.Domain.Models.Content;
using Salons_Project.Domain.Models.Organization;
using Salons_Project.Domain.Models.Users;

namespace Salons_Project.Infrastructure.Persistence;

public class DataContext : DbContext, IDataContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<PasswordVerification> PasswordVerifications => Set<PasswordVerification>();

    public DbSet<EmailVerification> EmailVerifications => Set<EmailVerification>();

    public DbSet<Favorite> Favorites => Set<Favorite>();

    public DbSet<Review> Reviews => Set<Review>();

    public DbSet<Procedure> Procedures => Set<Procedure>();

    public DbSet<Salon> Salons => Set<Salon>();

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}
