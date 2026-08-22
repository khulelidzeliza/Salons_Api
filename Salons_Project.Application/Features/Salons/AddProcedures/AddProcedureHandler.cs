using Microsoft.EntityFrameworkCore;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Domain.Common.Results;
using Salons_Project.Domain.Models.Organization;
using MediatR;

namespace Salons_Project.Application.Features.Salons.AddProcedures;

public sealed class AddProcedureHandler : IRequestHandler<AddProcedureCommand, Result>
{
    private readonly IDataContext _db;

    public AddProcedureHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<Result> Handle(AddProcedureCommand req, CancellationToken ct)
    {
        var salonExists = await _db.Salons.AnyAsync(s => s.Id == req.SalonId, ct);

        if (!salonExists)
            return Result.Failure(Errors.NotFound("Salon not found."));

        var procedure = new Procedure
        {
            ProcedureType = req.ProcedureType,
            Masters = req.Masters ?? new(),
            FinishedWorks = req.FinishedWorks ?? new(),
            SalonId = req.SalonId,
        };

        _db.Procedures.Add(procedure);
        await _db.SaveChangesAsync(ct);

        return Result.Success();
    }
}