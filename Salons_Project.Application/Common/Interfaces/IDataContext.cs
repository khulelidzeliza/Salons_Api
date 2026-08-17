



using Microsoft.EntityFrameworkCore;
using Salons_Project.Domain.Models.Auth;
using Salons_Project.Domain.Models.Content;
using Salons_Project.Domain.Models.Organization;
using Salons_Project.Domain.Models.Users;

namespace Salons_Project.Application.Common.Interfaces;

public interface IDataContext 
{
    DbSet<User> Users { get; }
    DbSet<PasswordVerification> PasswordVerifications { get; }
    DbSet<EmailVerification> EmailVerifications { get; }
    
    DbSet<Favorite> Favorites { get; }
    DbSet<Review> Reviews { get; }
    DbSet<Procedure> Procedures { get; }
    DbSet<Salon> Salons { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);

}
