using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> AddSalon(CreateSalonRequest req, CancellationToken ct)
    {
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

        var result = await _mediator.Send(cmd, ct);
        return Ok(result);
    }

    [HttpPost("add-procedure")]
    public async Task<IActionResult> AddProcedure(AddProcedureRequest req, CancellationToken ct)
    {
        var cmd = new AddProcedureCommand(
            req.ProcedureType,
            req.Masters,    
            req.FinishedWorks,
            req.SalonId
        );

        var result = await _mediator.Send(cmd, ct);
        return Ok(result);
    }
}
