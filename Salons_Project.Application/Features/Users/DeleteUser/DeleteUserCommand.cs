using MediatR;
using Salons_Project.Domain.Common.Results;

namespace Salons_Project.Application.Features.Users.DeleteUser;

public sealed record DeleteUserCommand(
    int userId
) : IRequest<Result>;
