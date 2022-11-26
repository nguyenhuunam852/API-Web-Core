#nullable disable

using MyWebApp;

namespace MyWebApp.DTO.User.Response
{
    public class UserPagiListResponseDTO
    {
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public string FilterParam { get; set; }
        public UserInfoDto[] UserInfoDtos { get; set; }
    }
}
