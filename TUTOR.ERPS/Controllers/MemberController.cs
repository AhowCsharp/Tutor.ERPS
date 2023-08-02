using TUTOR.ERPS.API.Infa;
using TUTOR.ERPS.API.Infa.Attribute;
using TUTOR.Biz.Domain.API;
using TUTOR.Biz.Models;
using TUTOR.Biz.Models.Responses.Member;
using TUTOR.Biz.Utility;
using Microsoft.AspNetCore.Mvc;
using TUTOR.ERPS.Api.Infra.Nlog;
using NPOI.SS.Formula.Functions;

namespace IALW.API.ApiControllers
{
    /// <response code="401">驗證失敗</response>
    [Route("Member")]
    [ApiController]
    [ApTokenAuth]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status401Unauthorized)]
    public class MemberController : BaseController
    {
        private readonly ILogger<MemberController> logger;
        private readonly SystemLogger systemLogger;
        private readonly MemberDomain domain;

        public MemberController(ILogger<MemberController> logger, MemberDomain domain, SystemLogger systemLogger)
        {
            this.logger = logger;
            this.domain = domain;
            this.systemLogger = systemLogger;
        }

        /// <summary>
        /// 批次匯入學生資料
        /// </summary>

        /// <returns></returns>
        [HttpGet]
        [Route("ImportFromExcel")]
        [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ImportFromExcel(IFormFile file)
        {
            try
            {
                var response = domain.ImportFromExcel(file);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "匯入出錯 資料錯誤");

                return ServerError500();
            }
        }

        /// <summary>
        /// 提單條款文件資料
        /// </summary>
        /// <param name="page">頁數</param>
        /// <response code="200">OK</response>
        /// <response code="400">後端驗證錯誤、少參數、數值有誤、格式錯誤</response>
        /// <response code="500">內部錯誤</response>
        /// <returns></returns>
        [HttpGet]
        [Route("StudentList")]
        [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Data(int page = 1)
        {
            try
            {
                var response = domain.GetList(page);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "學生資料 資料錯誤");
                systemLogger.Error("學生資料 資料錯誤", ex);
                return ServerError500();
            }
        }
    }
}