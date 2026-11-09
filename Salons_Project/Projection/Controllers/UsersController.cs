using MediatR;
using Microsoft.AspNetCore.Mvc;
using Salons_Project.Api.Extensions.Auth;
using Salons_Project.Application.Features.Users.DeleteUser;
using Salons_Project.Application.Features.Users.UpdateUser;

namespace Salons_Project.Api.Projection.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator) => _mediator = mediator;

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(CancellationToken ct)
        {
            var userId = this.GetUserId();

            var result = await _mediator.Send(new DeleteUserCommand(userId), ct);
            return result.IsSuccess ? NoContent() : NotFound(result);
        }
        
        [HttpPatch("update-user")]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest req, CancellationToken ct)
        {
            var userId = this.GetUserId();

            var cmd = new UpdateUserCommand(userId, req.Age, req.UserName, req.PhoneNumber, req.City);

            var result = await _mediator.Send(cmd, ct);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }

