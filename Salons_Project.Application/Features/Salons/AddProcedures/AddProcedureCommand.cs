using MediatR;
using Salons_Project.Domain.Common.Results;
using Salons_Project.Domain.Enums.Organization;

namespace Salons_Project.Application.Features.Salons.AddProcedures;

public sealed record AddProcedureCommand(
   ProcedureType ProcedureType,
   List<string> Masters,
   List<string> FinishedWorks,
   int SalonId
) : IRequest<Result>;
