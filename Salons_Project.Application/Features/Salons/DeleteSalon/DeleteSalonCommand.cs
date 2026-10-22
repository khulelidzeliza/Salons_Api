
using MediatR;
using Salons_Project.Domain.Common.Results;

namespace Salons_Project.Application.Features.Organization.DeleteSalon;
    public sealed record DeleteSalonCommand(
        int userid,
        int SalonId
    ) : IRequest<Result>;
