using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyWebApp.DTO;
using MyWebApp.DTO.User.Request;
using MyWebApp.DTO.User.Response;
using MyWebApp.Interface;
using MyWebApp.Models;
using MyWebApp.Shared;
using System.Net;

namespace MyWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController: ControllerBase
    {
        private readonly IUserService _userInterface;
        private readonly IAuthorizeJwt _authorizeJwt;
        private readonly Dictionary<int, Tuple<int, List<string>>> _listErrors = new Dictionary<int, Tuple<int, List<string>>>()
        {
            { 1, new Tuple<int, List<string>>(StatusCodes.Status422UnprocessableEntity, new List<string>() { "Confirm Password Fail" }) },
            { 2, new Tuple<int, List<string>>(StatusCodes.Status409Conflict, new List<string>() { "User Exist!" }) },
            { 3, new Tuple<int, List<string>>(StatusCodes.Status500InternalServerError, new List<string>() { "Server Errors!" }) },
            { 4, new Tuple<int, List<string>>(StatusCodes.Status404NotFound, new List<string>() { "User Login Fail!" }) },
        };

        public authController(IAuthorizeJwt authorizeJwt,IUserService userInterface, IOptions<AppSettings> appSettings)
        {
            _userInterface = userInterface;
            _authorizeJwt = authorizeJwt;
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public CustomResponse Register([FromBody] RequestRegisterUserDTO requestUserDTO)
        {
            try
            {
                if (requestUserDTO.UserPassword != requestUserDTO.ConfirmPassword)
                    return new CustomResponse(_listErrors[1]);

                if (this._userInterface.getUserByUserName(requestUserDTO.UserName) != null)
                    return new CustomResponse(_listErrors[2]);

                UserModel? newUser = this._userInterface.RegisterNewUser(requestUserDTO);

                if (newUser == null) 
                    return new CustomResponse(_listErrors[3]);

                else return new CustomResponse(content: new ResponseRegisterDTO()
                   {
                       UserId = newUser.UserId,
                       UserName = newUser.UserName,
                       Email = newUser.Email,
                       Jwt = string.Empty
                   }
                );
            }
            catch
            {
                return new CustomResponse(_listErrors[3]);
            }
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public CustomResponse Login([FromBody] RequestUserLoginDTO requestUserDTO)
        {
            try
            {
                UserModel? newUser = this._userInterface.getUserByLogin(requestUserDTO);

                if (newUser == null) return new CustomResponse(_listErrors[4]);
                else
                {
                    var token = this._authorizeJwt.generateJwtToken(newUser);
                    return new CustomResponse(content: new ResponseLoginUserDTO()
                    {
                        Jwt = token
                    });
                }
            }
            catch
            {
                return new CustomResponse(_listErrors[3]);
            }
        }
    }
}
