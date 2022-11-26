using System.Text.Json.Serialization;

namespace MyWebApp.DTO.User.Response
{
    public class UserInfoDto
    {
        public int UserId { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? FullName { get; set; }
    }
}
