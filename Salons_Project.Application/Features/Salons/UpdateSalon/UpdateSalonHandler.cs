using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Salons_Project.Application.Common.Contracts;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Application.Features.Upload_Picture;
using Salons_Project.Domain.Common.Results;

namespace Salons_Project.Application.Features.Salons.UpdateSalon;

public class UpdateSalonHandler : IRequestHandler<UpdateSalonCommand, Result>
{
    private const string PicturesFolder = "salons";
 
    private readonly IDataContext _db;
    private readonly IUploadService _files;
 
    public UpdateSalonHandler(IDataContext db, IUploadService files)
    {
        _db = db;
        _files = files;
    }
 
    public async Task<Result> Handle(UpdateSalonCommand req, CancellationToken ct)
    {
        var salon = await _db.Salons.FirstOrDefaultAsync(s => s.UserId == req.UserId, ct);
 
        if (salon is null) return Result.Failure(Errors.NotFound("Salon not found."));
 
        salon.GeorgianName = req.GeorgianName;
        salon.EnglishName = req.EnglishName;
        salon.Location = req.Location;
        salon.PhoneNumber = req.PhoneNumber;
        salon.Email = req.Email;
        salon.Price = req.Price;
        salon.FacebookLink = req.FacebookLink ?? "";
        salon.InstagramLink = req.InstagramLink ?? "";
        salon.WorkingHours = req.WorkingHours ?? "";
 
        var newPictures = await _files.UploadAsync(req.Pictures, PicturesFolder, ct);
        var oldPictures = new List<string>();
 
        if (newPictures.Count > 0)
        {
            oldPictures = salon.Pictures;
            salon.Pictures = newPictures;
        }
 
        try
        {
            await _db.SaveChangesAsync(ct);
        }
        catch
        {
            _files.Delete(newPictures);
            throw;
        }
 
        _files.Delete(oldPictures);
 
        return Result.Success();
    }
}