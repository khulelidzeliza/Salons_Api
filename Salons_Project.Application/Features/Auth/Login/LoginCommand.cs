



using MediatR;
using Salons_Project.Application.Common.Contracts;
using Salons_Project.Domain.Common.Results;

namespace Salons_Project.Application.Features.Auth.Login;

public sealed record LoginCommand(
   string Email,
   string Password
) : IRequest<Result<TokenResponse>>;
