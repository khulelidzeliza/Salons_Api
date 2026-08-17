



using MediatR;
using Salons_Project.Application.Common.Contracts;
using Salons_Project.Domain.Common.Results;

namespace Salons_Project.Application.Features.Auth.VerifyEmail;

public sealed record VerifyEmailCommand(
    string email,
    string code
    )
    : IRequest<Result<TokenResponse>>;