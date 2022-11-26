using Microsoft.AspNetCore.Mvc;
using MyWebApp.Attributes;
using MyWebApp.DTO;
using MyWebApp.DTO.State.Request;
using MyWebApp.DTO.User.Response;
using MyWebApp.Interface;
using MyWebApp.Models;

#nullable disable

namespace MyWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly IUserService _userInterface;
        private readonly IStateService _stateInterface;

        public userController(IUserService userInterface, IStateService stateInterface)
        {
            _userInterface = userInterface;
            _stateInterface = stateInterface;
        }

        [Route("getlist")]
        [AttributeJwt]
        [HttpGet]
        public CustomResponse GetList([FromQuery] StateRequestDto requestDto)
        {
            try
            {
                var account = (UserModel)HttpContext.Items["Account"];

                var getCurrentState = this._stateInterface.GetState(requestDto, account.UserId);

                if(getCurrentState == null) return new CustomResponse(404, new List<string>() { "Sort Order Not Exist!" });

                var tupleInfo = this._userInterface.getAllUsersWithFilters(getCurrentState);

                var responseListUser = new UserPagiListResponseDTO()
                {
                    Pages = tupleInfo.Item1,
                    CurrentPage = getCurrentState.Page + 1,
                    FilterParam = getCurrentState.FilterParam,
                    UserInfoDtos = tupleInfo.Item2.Select(item => new UserInfoDto()
                    {
                        UserId = item.UserId,
                        UserName = item.UserName,
                        Email = item.Email,
                        FullName = item.FullName
                    }).ToArray()
                };

                return new CustomResponse(content: responseListUser);
            }
            catch (Exception e)
            {
                return new CustomResponse(500, new List<string>() { "Server Errors!" });
            }
        }
    }
}
