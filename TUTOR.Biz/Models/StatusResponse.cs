using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace TUTOR.Biz.Models
{
    public class StatusResponse
    {
        public StatusResponse(IList<string> errorMsg, HttpStatusCode statusCode)
        {
            ErrorMsg = errorMsg;
            StatusCode = statusCode;
        }

        [SwaggerSchema("網路狀態碼")]
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        [SwaggerSchema("錯誤訊息")]
        public IList<string> ErrorMsg { get; set; }
    }
}