using HotChocolate.Authorization;
using MediatR;
using Salons_Project.Application.Features.Users.Queries.GetUser;
using Salons_Project.Domain.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Salons_Project.Api.Controllers.graphql.users;


[QueryType]
public sealed class UserQueries
{
    [Authorize]
    [UseProjection]
    public async Task<IQueryable<User>> Me(
        IMediator mediator,
        CancellationToken ct,
        IHttpContextAccessor http
    )
    {
        var userIdClaim = http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            throw new GraphQLException("User not found!");

        if (!int.TryParse(userIdClaim, out int userId))
            throw new ValidationException("User ID claim is not a valid integer.");

        return await mediator.Send(new GetUserByIdQueryable(userId), ct);
    }


}
