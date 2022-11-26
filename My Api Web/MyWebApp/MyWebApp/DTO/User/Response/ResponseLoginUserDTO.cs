using Newtonsoft.Json;

namespace MyWebApp.DTO.User.Response
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ResponseLoginUserDTO : CustomResponse
    {
        public string? Jwt { get; set; }
    }
}
