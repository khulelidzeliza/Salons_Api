



using MediatR;
using Salons_Project.Domain.Common.Results;

namespace Salons_Project.Application.Features.Auth.ResetPassword;

public sealed record ResetPasswordCommand(
string Email)
: IRequest<Result<bool>>;