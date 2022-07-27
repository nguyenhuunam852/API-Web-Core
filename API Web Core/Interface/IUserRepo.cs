using API_Web_Core.DTO;
using API_Web_Core.DTO.Auth;
using API_Web_Core.DTO.User;
using API_Web_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Web_Core.Repository
{
    public interface IUserRepo
    {
        Login_Response_dto GetUserbyLogin(Login_Request_dto userMode);
        User_DTO GetUserbyId(int id);
        IEnumerable<GetRoles> getRolesbyUserId(int userId);  
    }
}
