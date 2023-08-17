using TUTOR.ERPS.API.Infa;
using TUTOR.ERPS.API.Infa.Attribute;
using TUTOR.Biz.Domain.API;
using TUTOR.Biz.Models;
using TUTOR.Biz.Models.Responses.SentenceManage;
using TUTOR.Biz.Utility;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using TUTOR.Biz.Models.Requests;

namespace TUTOR.ERPS.API
{
    /// <response code="401">驗證失敗</response>
    [Route("sentence")]
    [ApiController]
    [ApTokenAuth]
    public class SentenceManageController : BaseController
    {
        private readonly ILogger<SentenceManageController> _logger;
        private readonly SentenceManageDomain _domain;

        public SentenceManageController(ILogger<SentenceManageController> logger, SentenceManageDomain domain)
        {
            _logger = logger;
            _domain = domain;
        }

        /// <summary>
        /// 取得句子清單
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpGet]
        [Route("info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSentenceManageList()
        {
            try
            {
                var response = await _domain.GetSentenceManageListAsync(); //GetSentenceManageListAsync
                _logger.LogInformation("取得有效會員清單");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(GetSentenceManageList));
                return ServerError500();
            }
        }

        /// <summary>
        /// 取得句子清單
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpGet]
        [Route("type")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSentenceTypeList()
        {
            try
            {
                var response = await _domain.GetSentenceTypeListAsync(); //GetSentenceManageListAsync
                _logger.LogInformation("取得句子類型清單");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(GetSentenceManageList));
                return ServerError500();
            }
        }

        /// <summary>
        /// 取得句子清單
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpPost]
        [Route("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSentenceManage([FromForm] SentenceManageRequest request)
        {
            try
            {
                if (request.Mp3File == null || request.Mp3File.Length == 0)
                {
                    return BadRequest("No file provided.");
                }
                var response = await _domain.CreateSentenceManageAsync(request); //GetSentenceManageListAsync
                if (response)
                {
                    _logger.LogInformation("新增句子");
                    return Ok(response);
                }
                else
                {
                    return BadRequest("fileName error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(GetSentenceManageList));
                return ServerError500();
            }
        }
    }
}