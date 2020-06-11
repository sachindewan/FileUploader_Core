using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiCore.Helpers
{
    public class AutherizeCurrentUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.RouteValues.ContainsKey("userId"))
            {
                var currentUserIdPassedIn = int.Parse(context.HttpContext.Request.RouteValues["userId"].ToString());
                var userId = int.Parse(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (userId != currentUserIdPassedIn)
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.HttpContext.Response.WriteAsync("Invalid user... Autherization failed");
                }
                else
                {
                    await next();
                }
            }

            else
            {
                await next();
            }

        }

    }
}
