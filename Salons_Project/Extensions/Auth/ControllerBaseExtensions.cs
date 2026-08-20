using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Salons_Project.Api.Extensions.Auth
{
    public static class ControllerBaseExtensions
    {
        public static int GetCurrentUserId(this ControllerBase controller)
        {
            var userId = controller.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            return id;
        }
        public static int? GetNullableUserId(this ControllerBase controller)
        {
            var userId = controller.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userId, out int id))
            {
                return id;
            }
            else
            {
                return null;
            }
        }
    }
}
