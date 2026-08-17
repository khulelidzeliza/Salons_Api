



using MediatR;
using Salons_Project.Domain.Common.Results;

namespace Salons_Project.Application.Features.Favorites.CreateFavorite;

public record CreateFavoriteCommand(
    int userid,
    int salonid
    ) : IRequest<Result>;