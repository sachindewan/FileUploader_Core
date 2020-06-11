using Microsoft.AspNetCore.Http;
using System;

namespace WebApiCore.Helpers
{
    public static class Extentions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
        public static int CalculateAge(this DateTime dateOfUser)
        {
            var age = DateTime.Today.Year - dateOfUser.Year;
            if (dateOfUser.AddDays(age) > DateTime.Today)
            {
                age--;
            }
            return age;
        }
    }
}
