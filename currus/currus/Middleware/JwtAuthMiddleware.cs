using currus.Logging.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace currus.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                var authHeader = httpContext.Request.Headers.Authorization.ToString();
                if (authHeader.Equals(""))
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsJsonAsync("Invalid User Key");
                    return;
                }
                var parts = authHeader.Split(' ');
                if (parts.Length < 2)
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsJsonAsync("Invalid User Key");
                    return;
                }
                var accessToken = parts[1];
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(accessToken);
                DateTime validTo = token.ValidTo;
                if(DateTime.Now > validTo.AddHours(2))
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsJsonAsync("User token expired. You have to log in again.");
                    return;
                }
                var payload = token.Payload;
                string emailPattern = @"^([a-zA-Z0-9_\-\.]+)@(([a-zA-Z0-9\-]+\.)+)([a-zA-Z]{2,4}|[0-9]{1,3})$";
                object? email;
                payload.TryGetValue("email", out email);
                if (email != null)
                {
                    string? str = email.ToString();
                    if (Regex.IsMatch(str ?? "", emailPattern, RegexOptions.IgnoreCase))
                    {
                        httpContext.Items["email"] = email;
                        await _next(httpContext);
                    }
                }
                else
                {
                    await httpContext.Response.WriteAsync("Invalid User Key");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message + ": " + ex.StackTrace);
                return;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtAuthMiddleware>();
        }
    }
}
