using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Web_Core.DTO.Auth
{
    public class Login_Request_dto
    {
        public string user_name { get; set; }
        public string user_password { get; set; }

    }
}
