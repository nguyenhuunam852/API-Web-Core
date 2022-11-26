using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyWebApp.Interface;
using MyWebApp.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MyWebApp.Middlewares
{
    public class JwtAuthorize
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly IUserService _userInterface;
        private string _adminToken = "Admin";

        public JwtAuthorize(RequestDelegate next, IOptions<AppSettings> appSettings,IUserService userInterface)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _userInterface = userInterface;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await attachAccountToContext(context, token);

            await _next(context);
        }

        private async Task attachAccountToContext(HttpContext context, string token)
        {
            try
            {
                if (token.Equals(_adminToken, StringComparison.OrdinalIgnoreCase))
                {
                    context.Items["Account"] = await this._userInterface.getUserByUserIdAsync(1);
                }
                else
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "user_id").Value);

                    context.Items["Account"] = await this._userInterface.getUserByUserIdAsync(accountId);
                }
            }
            catch
            {
              
            }
        }
    }
}
