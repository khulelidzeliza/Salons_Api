using MediatR;
using Microsoft.AspNetCore.Http;
using Salons_Project.Domain.Common.Results;
using Salons_Project.Domain.Enums.Organization;

namespace Salons_Project.Application.Features.Salons.AddProcedures;

public sealed record AddProcedureCommand(
   int userid,
   int SalonId,
   ProcedureType ProcedureType,
   List<string> Masters,
   List<IFormFile> FinishedWorks
) : IRequest<Result>;
