using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Salons_Project.Application.Common.Contracts;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Domain.Common.Results;
using Salons_Project.Domain.Models.Organization;

namespace Salons_Project.Application.Features.Organization.CreateOrganization;

public class CreateSalonHandler : IRequestHandler<CreateSalonCommand, Result>
{
    private readonly IDataContext _db;

    public CreateSalonHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<Result> Handle(CreateSalonCommand req, CancellationToken ct)
    {
        var userExists = await _db.Users.AnyAsync(u => u.Id == req.UserId, ct);

        if(!userExists) return Result.Failure(Errors.NotFound("User not found."));
        var filepaths = await UploadPictures(req.Pictures);

        var salon = new Salon
        {
            Name = req.Name,
            Location = req.Location,
            PhoneNumber = req.PhoneNumber,  
            Email = req.Email,
            Price = req.Price,
            FacebookLink = req.FacebookLink ?? "",
            InstagramLink = req.InstagramLink ?? "",
            UserId = req.UserId,
            Pictures = filepaths ,
            WorkingHours = req.workingHours ?? "",
        };

        _db.Salons.Add(salon);
        await _db.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<List<string>> UploadPictures(List<IFormFile> pics)
    {
        var filenames = new List<string>();

        if(pics is null || pics.Count == 0) return filenames;

        var upload = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "salons");
        Directory.CreateDirectory(upload);

        foreach (var pic in pics)
        {
            if (pic.Length == 0) continue;

            var file = $"{Guid.NewGuid()}{Path.GetExtension(pic.FileName)}";
            var path = Path.Combine(upload, file);

            using var stream = new FileStream(path, FileMode.Create);
            await pics[0].CopyToAsync(stream);

            filenames.Add($"/uploads/salons/{file}");
        }
            return filenames;
    }
}
