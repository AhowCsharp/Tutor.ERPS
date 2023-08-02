using TUTOR.Biz.Domain.API;
using TUTOR.Biz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TUTOR.ERPS.API.Infa.Attribute
{
    public class ApTokenAuth : System.Attribute, IAsyncAuthorizationFilter
    {
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var headers = context.HttpContext.Request.Headers;
            var logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<ApTokenAuth>)) as ILogger;

            if (!headers.ContainsKey("X-Ap-Token") || string.IsNullOrEmpty(headers["X-Ap-Token"].ToString()))
            {
                logger.LogInformation("缺少Token");

                var response = new BadRequestResponse(new string[] { "缺少Token" });
                context.Result = new ObjectResult(response) { StatusCode = StatusCodes.Status400BadRequest };
            }
            else
            {
                var token = headers["X-Ap-Token"].ToString();
                var ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
                var attributeDomain = context.HttpContext.RequestServices.GetService(typeof(AttributeDomain)) as AttributeDomain;                
                if (!attributeDomain.VerifyToken(token, ipAddress))
                {
                    
                    logger.LogInformation("Token驗證失敗");

                    var response = new BadRequestResponse(new string[] { "Token驗證失敗" });
                    context.Result = new ObjectResult(response) { StatusCode = StatusCodes.Status401Unauthorized };
                }

                attributeDomain.TokenLog(token);
            }

            return Task.FromResult(context.Result);
        }
    }
}
