using MediatR;
using Salons_Project.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salons_Project.Application.Features.Auth.VerifyEmail.Verification;

public sealed record ResendVerificationCommand(
   string Email) : IRequest<Result<bool>>;
