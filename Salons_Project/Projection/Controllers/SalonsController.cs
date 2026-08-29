using MediatR;
using Microsoft.AspNetCore.Mvc;
using Salons_Project.Api.Extensions.Auth;
using Salons_Project.Application.Features.Organization.CreateOrganization;
using Salons_Project.Application.Features.Salons.AddProcedures;
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
        //var userid = this.GetUserId();

        var cmd = new CreateSalonCommand(
            req.Name,
            req.Location,
            req.PhoneNumber,
            req.Email,
            req.Price,
            req.FacebookLink,
            req.InstagramLink,
            req.Pictures,
            req.UserId,
            req.WorkingHours
        );
        //var updatedCmd = cmd with { UserId = userid };

        //var result = await _mediator.Send(updatedCmd, ct);
        var result = await _mediator.Send(cmd, ct);
        return Ok(result);
    }   

    [HttpPost("add-procedure")]
    public async Task<IActionResult> AddProcedure([FromForm] AddProcedureRequest req, CancellationToken ct)
    {
        //var userid = this.GetUserId();

        var cmd = new AddProcedureCommand(
            req.userid,
            req.SalonId,
            req.ProcedureType,
            req.Masters,    
            req.FinishedWorks
        );
        //var updatedCmd = cmd with { userid = userid };
        var result = await _mediator.Send(cmd, ct);

        //var result = await _mediator.Send(updatedCmd, ct);
        return Ok(result);
    }
}
