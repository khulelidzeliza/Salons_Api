using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Salons_Project.Api.Extensions.Auth;
using Salons_Project.Application.Features.Favorites.CreateFavorite;

namespace Salons_Project.Api.Projection.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReviewsController(IMediator mediator) => _mediator = mediator;


   
}
