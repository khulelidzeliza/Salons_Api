
using MediatR;
using Salons_Project.Domain.Common.Results;

namespace Salons_Project.Application.Features.Users.UpdateUser;
    public record UpdateUserCommand(
        int UserId,
        int Age,
        string UserName,
        string? PhoneNumber,
        string? City) : IRequest<Result>;
