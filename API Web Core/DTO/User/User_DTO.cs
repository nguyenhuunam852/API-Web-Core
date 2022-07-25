using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Web_Core.DTO.User
{
    public class User_DTO
    {
        public User_DTO()
        {
            //this.userid = userid;
            //this.user_name = user_name;
        }

        public string user_name { get; set; }
        public int userid { get; set; }

    }
}
