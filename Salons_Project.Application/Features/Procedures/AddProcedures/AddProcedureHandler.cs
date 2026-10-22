using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Domain.Common.Results;
using Salons_Project.Domain.Models.Organization;

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
        var filepaths = await UploadPictures(req.FinishedWorks);

        if (!salonExists)
            return Result.Failure(Errors.NotFound("Salon not found."));

        var procedure = new Procedure
        {
            ProcedureType = req.ProcedureType,
            Masters = req.Masters ?? new(),
            FinishedWorks = filepaths,
            SalonId = req.SalonId,
        };

        _db.Procedures.Add(procedure);
        await _db.SaveChangesAsync(ct);

        return Result.Success();
    }


    public async Task<List<string>> UploadPictures(List<IFormFile> pics)
    {
        var filenames = new List<string>();

        if (pics is null || pics.Count == 0) return filenames;

        var upload = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "procedures");
        Directory.CreateDirectory(upload);

        foreach (var pic in pics)
        {
            if (pic.Length == 0) continue;

            var file = $"{Guid.NewGuid()}{Path.GetExtension(pic.FileName)}";
            var path = Path.Combine(upload, file);

            using var stream = new FileStream(path, FileMode.Create);
            await pics[0].CopyToAsync(stream);
            filenames.Add($"/uploads/procedures/{file}");
        }
        return filenames;
    }
}