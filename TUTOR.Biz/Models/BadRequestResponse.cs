using Swashbuckle.AspNetCore.Annotations;

namespace TUTOR.Biz.Models
{
    public class BadRequestResponse
    {
        public BadRequestResponse(IList<string> errorMsg)
        {
            ErrorMsg = errorMsg;
        }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        [SwaggerSchema("錯誤訊息")]
        public IList<string> ErrorMsg { get; set; }
    }
}
