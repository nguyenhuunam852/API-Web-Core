using System.Text.Json.Serialization;

namespace MyWebApp.DTO.User.Response
{
    public class ResponseRegisterDTO
    {
        public int UserId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? UserName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Email { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Jwt { get; set; }
    }
}
