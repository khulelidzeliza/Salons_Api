using MediatR;
using Microsoft.AspNetCore.Mvc;
using Salons_Project.Api.Extensions.Auth;
using Salons_Project.Application.Features.Organization.DeleteSalon;
using Salons_Project.Application.Features.Salons.AddProcedures;

namespace Salons_Project.Api.Projection.Controllers;

public class ProcedureController : Controller
{
    private readonly IMediator _mediator;
    public ProcedureController(IMediator mediator) => _mediator = mediator;
    
    [HttpPost("add-procedure")]
    public async Task<IActionResult> AddProcedure([FromForm] AddProcedureRequest req, CancellationToken ct)
    {
        var userid = this.GetUserId();

        var cmd = new AddProcedureCommand(
            req.userid,
            req.SalonId,
            req.ProcedureType,
            req.Masters,    
            req.FinishedWorks
        );
        var updatedCmd = cmd with { userid = userid };
        //var result = await _mediator.Send(cmd, ct);

        var result = await _mediator.Send(updatedCmd, ct);
        return Ok(result);
    }
    
    
    [HttpDelete("delete-procedure")]
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
}