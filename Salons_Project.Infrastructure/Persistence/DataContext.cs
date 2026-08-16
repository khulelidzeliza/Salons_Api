
using Microsoft.EntityFrameworkCore;
using Salons_Project.Application.Common.Interfaces;

namespace Salons_Project.Infrastructure.Persistence;

public class DataContext : DbContext , IDataContext 
{

}
