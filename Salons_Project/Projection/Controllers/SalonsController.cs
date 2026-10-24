using MediatR;
using Microsoft.AspNetCore.Mvc;
using Salons_Project.Api.Extensions.Auth;
using Salons_Project.Application.Features.Organization.CreateOrganization;
using Salons_Project.Application.Features.Organization.DeleteSalon;
using Salons_Project.Application.Features.Salons.UpdateSalon;

namespace Salons_Project.Api.Projection.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalonsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalonsController(IMediator mediator) => _mediator = mediator;



    [HttpPost("add-salon")]
    public async Task<IActionResult> AddSalon([FromForm] CreateSalonRequest req, CancellationToken ct)
    {
        var userid = this.GetUserId();

        var cmd = new CreateSalonCommand(
            req.GeorgianName,
            req.EnglishName,
            req.Location,
            req.PhoneNumber,
            req.Email,
            req.Description,
            req.Price,
            req.FacebookLink,
            req.InstagramLink,
            req.Pictures,
            req.UserId,
            req.WorkingHours
        );
        var updatedCmd = cmd with { UserId = userid };

        var result = await _mediator.Send(updatedCmd, ct);

        return Ok(result);
    }

    [HttpDelete("delete-salon")]
    public async Task<IActionResult> DeleteSalon(DeleteSalonCommand request, CancellationToken ct)
    {
        var userid = this.GetUserId();

        var cmd = new DeleteSalonCommand(
            request.userid,
            request.SalonId);
        
        var updatedCmd = cmd with { userid = userid };
        var result = await _mediator.Send(cmd, ct);
        return Ok(result);
    }

    [HttpPut("update-salon")]
    public async Task<IActionResult> UpdateSalon(UpdateSalonCommand request, CancellationToken ct)
    {
        var userid = this.GetUserId();

        var cmd = new UpdateSalonCommand(
            request.UserId,
            request.Price,
            request.FacebookLink,
            request.InstagramLink,
            request.GeorgianName,
            request.EnglishName,
            request.Email,
            request.Description,
            request.Location,
            request.PhoneNumber,
            request.WorkingHours
        );
        var updatedCmd = cmd with { UserId = userid };
        var result = await _mediator.Send(cmd, ct);
        return Ok(result);
    }
}
