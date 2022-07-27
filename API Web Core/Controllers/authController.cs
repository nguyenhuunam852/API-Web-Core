using API_Web_Core.DTO;
using API_Web_Core.DTO.Auth;
using API_Web_Core.DTO.User;
using API_Web_Core.Helpers;
using API_Web_Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Web_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserRepo _userRepository;
       
        public authController(IConfiguration config, IUserRepo userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public ActionResult<Login_Response_dto> Login(Login_Request_dto userModel)
        {
            if (string.IsNullOrEmpty(userModel.user_name) || string.IsNullOrEmpty(userModel.user_password))
            {
                return (RedirectToAction("Error"));
            }

            var validUser = _userRepository.GetUserbyLogin(userModel);

            if (validUser != null)
            {
                return Ok(validUser);
            }
            else
            {
                return Unauthorized();
            }
        }

        [JwtAuthorize]
        [Route("roles")]
        [HttpGet]
        public ActionResult<Object> GetRoles()
        {
            User_DTO userDto = (User_DTO)HttpContext.Items["User"];
            IEnumerable<string> getRoles = _userRepository.getRolesbyUserId(userDto.userid).Select(x=>x.Key);
            return Ok(getRoles);

        }

    }
}
