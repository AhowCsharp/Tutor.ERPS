using TUTOR.Biz.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TUTOR.Biz.Utility
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        protected virtual ObjectResult Ok200()
        {
            return StatusCode(StatusCodes.Status200OK, null);
        }

        [NonAction]
        public virtual ObjectResult ServerError500()
        {
            return StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [NonAction]
        public virtual ObjectResult Forbidden403(IList<string> errormsg = null)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new BadRequestResponse(errormsg));
        }

        [NonAction]
        public virtual ObjectResult BadRequest400(IList<string> errormsg = null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new BadRequestResponse(errormsg));
        }

        [NonAction]
        public virtual ObjectResult Unauthorized401(IList<string> errormsg = null)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new BadRequestResponse(errormsg));
        }
    }
}
