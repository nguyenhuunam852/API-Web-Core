using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyWebApp.DTO;

namespace MyWebApp.Middlewares
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid) { 

                List<string> errorResponse = new List<string>();

                foreach (var item in context.ModelState.Values)
                {
                    errorResponse = errorResponse.Concat(item.Errors.Select(x => x.ErrorMessage)).ToList();
                }
                
                var errorMessage = new CustomResponse(422, errorResponse);
                
                context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                context.Result = new ObjectResult(errorMessage);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}