using API_Web_Core.DTO.User;
using API_Web_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Web_Core.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthorize : Attribute, IAuthorizationFilter
    {
        public JwtAuthorize()
        {
        }
        public JwtAuthorize(string? test)
        {
            var test1 = test;
            var test2 = 1;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User_DTO)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
