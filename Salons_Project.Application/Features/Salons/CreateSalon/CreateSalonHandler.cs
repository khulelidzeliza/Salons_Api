using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Salons_Project.Application.Common.Contracts;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Application.Features.Upload_Picture;
using Salons_Project.Domain.Common.Results;
using Salons_Project.Domain.Models.Organization;

namespace Salons_Project.Application.Features.Organization.CreateOrganization;

public class CreateSalonHandler : IRequestHandler<CreateSalonCommand, Result>
{
    private const string PicturesFolder = "salons";
    
    private readonly IDataContext _db;
    private readonly IUploadService _files;

    public CreateSalonHandler(IDataContext db  , IUploadService files)
    {
        _db = db;
        _files = files;
    }

    public async Task<Result> Handle(CreateSalonCommand req, CancellationToken ct)
    {
        var userExists = await _db.Users.AnyAsync(u => u.Id == req.UserId, ct);
 
        if (!userExists) return Result.Failure(Errors.NotFound("User not found."));
 
        var filepaths = await _files.UploadAsync(req.Pictures, PicturesFolder, ct);
 
        var salon = new Salon
        {
            GeorgianName = req.georgianName,
            EnglishName = req.englishName,
            Location = req.Location,
            PhoneNumber = req.PhoneNumber,
            Email = req.Email,
            Description = req.Description,
            Price = req.Price,
            FacebookLink = req.FacebookLink ?? "",
            InstagramLink = req.InstagramLink ?? "",
            UserId = req.UserId,
            Pictures = filepaths,
            WorkingHours = req.workingHours ?? "",
        };
 
        _db.Salons.Add(salon);
        
        await _db.SaveChangesAsync(ct);
 
        return Result.Success();
    }
   
}
