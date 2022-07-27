using API_Web_Core.DTO;
using API_Web_Core.DTO.Auth;
using API_Web_Core.DTO.User;
using API_Web_Core.Helpers;
using API_Web_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
//using System.Threading.Tasks;

namespace API_Web_Core.Repository
{
    public class User_Service : IUserRepo
    {
        private AppSettings _appSettings;

        public User_Service(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public IEnumerable<GetRoles> getRolesbyUserId(int userId)
        {

            using (var ctx = new dbContext())
            {
                var getRoles = ctx.GetRoles.FromSqlRaw(
                    @"WITH previous(Id, [Key]) AS (
                      SELECT role_id,
                             role_key
                      FROM   roles roles
                      WHERE  role_id = {0}
                      UNION ALL
                      SELECT curRoles.role_id,
                             curRoles.role_key
                      FROM   roles curRoles, previous pr
                      WHERE  curRoles.parent_id = pr.Id
                    )
                    SELECT previous.*
                    FROM   previous;
                ", userId);

                return getRoles.ToList();
            }
            //{
            //    var student = (from s in ctx.Roles 
            //                   join pivot in ctx.PivotUserRoles on s.RoleId equals pivot.RoleId
            //                   where s. == "Bill"
            //                   select s)
            //                  .Where(s => s.StudentName == name)
            //                  .FirstOrDefault<Student>();
            //}
            return null;
        }

        public Login_Response_dto GetUserbyLogin(Login_Request_dto userMode)
        {
            using (var context = new dbContext())
            {
                var user = context.Users.Where(x => x.UserName.ToLower() == userMode.user_name.ToLower() && x.UserPassword == userMode.user_password).FirstOrDefault();
                if (user == null) return null;
                var token = generateJwtToken(user);
                return new Login_Response_dto((User_DTO)user, token);
            }
        }

        public User_DTO GetUserbyId(int id)
        {
            using (var context = new dbContext())
            {
                return (User_DTO)context.Users.Where(x => x.UserId == id).FirstOrDefault();
            }
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("user_id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
