using API_Web_Core.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Web_Core.DTO
{
    public class Login_Response_dto
    {
        public Login_Response_dto(User_DTO _user,string token){
            this.user = _user;
            this.token = token;
        }
        public User_DTO user { get; set; }
        public string token{get;set;}
    }
}
