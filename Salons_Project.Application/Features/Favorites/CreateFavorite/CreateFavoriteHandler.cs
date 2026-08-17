



using MediatR;
using Microsoft.EntityFrameworkCore;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Domain.Common.Results;
using Salons_Project.Domain.Models.Content;

namespace Salons_Project.Application.Features.Favorites.CreateFavorite;
public class SaveBlogCommandHandler : IRequestHandler<CreateFavoriteCommand, Result>
{
    private readonly IDataContext _db;

    public SaveBlogCommandHandler(IDataContext db) => _db = db;
    public async Task<Result> Handle(CreateFavoriteCommand req, CancellationToken ct)
    {
        var user = await _db.Users.FindAsync(req.userid, ct);
        if (user == null)
            return Result.Failure(Errors.NotFound("User not found"));

        var blog = await _db.Salons.FindAsync(req.salonid, ct);
        if (blog == null)
            return Result.Failure(Errors.NotFound("Blog not found"));

        var saved = await _db.Favorites.FirstOrDefaultAsync
            (L => L.UserId == req.userid && L.SalonId == req.salonid, ct);

        if (saved != null)
        {
            user.UnFavoriteSalon(req.salonid);
            _db.Favorites.Remove(saved);
        }
        else
        {
            user.FavoriteSalon(req.salonid);
            var newsave = new Favorite
            {
                UserId = req.userid,
                SalonId = req.salonid,
                CreatedAt = DateTime.UtcNow
            };
            _db.Favorites.Add(newsave);
        }

        await _db.SaveChangesAsync(ct);
        return Result.Success();
    }
}
