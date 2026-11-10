using MediatR;
using Salons_Project.Domain.Common.Results;

namespace Salons_Project.Application.Features.Auth.ChangePassword;

public sealed record ChangePasswordCommand(
    string Token,
    string NewPassword,
    string ConfirmPassword)
    : IRequest<Result<bool>>;
