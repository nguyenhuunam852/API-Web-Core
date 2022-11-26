using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyWebApp.DTO;
using MyWebApp.Models;

#nullable disable

namespace MyWebApp.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AttributeJwt : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var account = (UserModel)context.HttpContext.Items["Account"];
            if (account == null)
            {
                context.Result = new JsonResult(new CustomResponse(404, new List<string>() { "User Not Valid!" }));
            }
        }
    }
}
