using System.ComponentModel.DataAnnotations;

#nullable disable

namespace MyWebApp.DTO.User.Request
{
    public class RequestUserLoginDTO
    {
        private const string _regexUserName = "^[0-9a-zA-Z]+$";
        private const string _regexPassword = "^[0-9a-fA-F]{32}$";

        [Required]
        [MinLength(5), MaxLength(100)]
        [RegularExpression(_regexUserName)]
        public string UserName { get; set; }

        [Required]
        [MinLength(5), MaxLength(100)]
        [RegularExpression(_regexPassword)]
        public string UserPassword { get; set; }
    }
}
