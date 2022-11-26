#nullable disable

using MyWebApp;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.DTO.User.Request
{
    public class RequestRegisterUserDTO
    {
        private const string _regexUserName = "^[0-9a-zA-Z]+$";
        private const string _regexPassword = "^[0-9a-fA-F]{32}$";
        private const string _regexEmail = "^(.+)@(.+)$";

        [Required]
        [MinLength(5), MaxLength(100)]
        [RegularExpression(_regexUserName)]
        public string UserName { get; set; }

        [Required]
        [MinLength(5), MaxLength(100)]
        [RegularExpression(_regexPassword)]
        public string UserPassword { get; set; }

        [MinLength(5), MaxLength(100)]
        public string FullName { get; set; }

        [MinLength(5), MaxLength(100)]
        [RegularExpression(_regexEmail)]
        public string Email { get; set; }

        [Required]
        [MinLength(5), MaxLength(100)]
        [RegularExpression(_regexPassword)]
        public string ConfirmPassword { get; set; }
    }
}
