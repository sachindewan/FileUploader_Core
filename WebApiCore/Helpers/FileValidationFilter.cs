using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Helpers
{
    public class FileValidationFilter: IAsyncActionFilter
    {
       public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var file = context.HttpContext.Request.Form.Files;
            if (file.Count > 0)
            {
                var contentType = file[0].ContentType;
                if (GetMimeTypes().ContainsValue(contentType))
                {
                    await next();
                }
                else
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.HttpContext.Response.WriteAsync("Invalid file type..only csv file will be allowed to processed");
                }
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.HttpContext.Response.WriteAsync("File lenght is or 0 or it was not selected");
            }
                    
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".xls", "application/vnd.ms-excel"},
                {".csv", "text/csv"}
            };
        }
    }
}

