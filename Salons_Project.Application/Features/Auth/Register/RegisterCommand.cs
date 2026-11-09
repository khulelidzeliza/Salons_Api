using MediatR;
using Salons_Project.Domain.Common.Results;
using Salons_Project.Domain.Enums.User;

namespace Salons_Project.Application.Features.Auth.Register;
public sealed record RegisterCommand(
    string Username,
    string Email,
    string Password,
    Roles Role
) : IRequest<Result>;

