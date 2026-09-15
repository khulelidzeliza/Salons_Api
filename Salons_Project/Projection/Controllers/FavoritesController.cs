using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Salons_Project.Api.Extensions.Auth;
using Salons_Project.Application.Features.Favorites.CreateFavorite;
using Salons_Project.Application.Features.Organization.CreateOrganization;

namespace Salons_Project.Api.Projection.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavoritesController : ControllerBase
{
    private readonly IMediator _mediator;

    public FavoritesController(IMediator mediator) => _mediator = mediator;


    [HttpPost("favorite-item")]
    public async Task<IActionResult> FavoriteItem(CreateFavoriteRequest req, CancellationToken ct)
    {
        var userid = this.GetUserId();

        var cmd = new CreateFavoriteCommand(
            req.userid,
            req.salonid
        );
        var updatedCmd = cmd with { userid = userid };

        var result = await _mediator.Send(updatedCmd, ct);

        return Ok(result);
    }
}
