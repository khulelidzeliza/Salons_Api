using MediatR;
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
        if (!userExists) return Result.Failure(Errors.Invalid("This account has already been verified."));

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
            Pictures = req.Pictures ?? new(),
            WorkingHours = req.workingHours ?? "",
        };

        _db.Salons.Add(salon);
        await _db.SaveChangesAsync(ct);

        return Result.Success();
    }
}
