using TUTOR.ERPS.API.Infa;
using TUTOR.ERPS.API.Infa.Attribute;
using TUTOR.Biz.Domain.API;
using TUTOR.Biz.Models;
using TUTOR.Biz.Models.Responses.Member;
using TUTOR.Biz.Utility;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using TUTOR.Biz.Models.Requests;

namespace TUTOR.ERPS.API
{
    /// <response code="401">驗證失敗</response>
    [Route("answerLog")]
    [ApiController]
    public class StudentAnswerLogController : BaseController
    {
        private readonly ILogger<StudentAnswerLogController> _logger;
        private readonly StudentAnswerLogDomain _domain;

        public StudentAnswerLogController(ILogger<StudentAnswerLogController> logger, StudentAnswerLogDomain domain)
        {
            _logger = logger;
            _domain = domain;
        }

        /// <summary>
        /// 驗證答案
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpPost]
        [Route("verify")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VerifyAnswer(StudentAnswerRequest StudentAnswerRequest)
        {
            try
            {
                var response = await _domain.VerifyAnswer(StudentAnswerRequest);
                if (response)
                {
                    _logger.LogInformation("驗證答案");
                    return Ok(response);
                }
                else 
                {
                    return BadRequest("驗證答案失敗");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(VerifyAnswer));
                return ServerError500();
            }
        }

    }
}