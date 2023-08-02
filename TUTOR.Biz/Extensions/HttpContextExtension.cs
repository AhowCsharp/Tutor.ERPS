using Microsoft.AspNetCore.Http;

namespace TUTOR.Biz.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetIP(this HttpContext httpContext)
        {
            return httpContext.Connection.RemoteIpAddress.ToString();
        }

        public static Uri GetAbsoluteUri(this HttpContext httpContext)
        {
            var request = httpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.Host;
            uriBuilder.Path = request.Path.ToString();
            uriBuilder.Query = request.QueryString.ToString();
            return uriBuilder.Uri;
        }
    }
}
