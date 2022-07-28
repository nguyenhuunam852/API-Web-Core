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

        public IEnumerable<GetPermissions> getRolesbyUserId(int userId)
        {

            using (var ctx = new dbContext())
            {
                var getRoles = ctx.GetPermissions.FromSqlRaw(
                    @"
                      WITH previous(Id, [Key], [path]) AS (
                      SELECT p.permission_id as Id,
                             p.permission_key as [Key],
                    		 '/' + convert(varchar(max), case when roles.parent_id is not null then roles.parent_id else 0 end) + '/' [path]
                      FROM   roles roles 
                      join PivotUserRole pur on roles.role_id = pur.RoleId
                      join PivotRolePermission prp on roles.role_id = prp.RoleId
                      join [permissions] p on permission_id = prp.PermissionId
                      WHERE  pur.UserId = {0} 
                      UNION ALL
                      SELECT curTable.permission_id as Id,
                    		 curTable.permission_key as [Key],
                    		 pr.[path] + convert(varchar(max),curTable.parent_id) + '/'
                      FROM   (
                         select curRoles.parent_id as parent_id,p.permission_id,p.permission_key from
                         roles curRoles 
                    	 join PivotRolePermission prp on curRoles.role_id = prp.RoleId
                         join [permissions] p on permission_id = prp.PermissionId
                      ) curTable, previous pr
                      WHERE  curTable.parent_id = pr.Id and pr.[path] not like '%/' + rtrim(curTable.parent_id) + '/%'
                    )
                    SELECT distinct previous.[Key]
                    FROM   previous;     
                ", userId);

                return getRoles.ToList();
            }
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
