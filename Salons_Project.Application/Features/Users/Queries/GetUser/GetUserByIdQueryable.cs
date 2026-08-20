using MediatR;
using Salons_Project.Domain.Models.Users;

namespace Salons_Project.Application.Features.Users.Queries.GetUser;

public sealed record GetUserByIdQueryable(int Id) : IRequest<IQueryable<User>>;
