using System.Net;

namespace MyWebApp.Middlewares
{
    public static class ErrorHandle
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(context => {
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "application/json";

                    return Task.CompletedTask;
                });
            });
        }
    }
}
