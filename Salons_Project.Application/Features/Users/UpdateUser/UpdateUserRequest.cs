
namespace Salons_Project.Application.Features.Users.UpdateUser;
    public class UpdateUserRequest
    {
        public int UserId { get; set; }
        public int Age { get; set; }
        public string UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
    }
