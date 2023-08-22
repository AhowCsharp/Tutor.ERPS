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
        /// 取得考試類型
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpGet]
        [Route("testinfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSentenceList(string type,string account,string name)
        {
            try
            {
                var response = await _domain.GetSentenceListAsync(type, account, name); //GetSentenceManageListAsync
                _logger.LogInformation("取得考試題型");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(GetSentenceList));
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
        /// 新增句子類型
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpPost]
        [Route("addType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddSentenceType(SentenceTypeRequest sentenceTypeRequest)
        {
            try
            {
                var response = await _domain.CreateSentenceTypeAsync(sentenceTypeRequest); //GetSentenceManageListAsync
                _logger.LogInformation("新增句子類型");

                if (response)
                {
                    _logger.LogInformation("新增句子類型");
                    return Ok(response);
                }
                else
                {
                    return BadRequest("句子類型已重複");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(AddSentenceType));
                return ServerError500();
            }
        }

        /// <summary>
        /// 刪除句子類型
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpDelete]
        [Route("removeType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveType(int id)
        {
            try
            {
                var response = await _domain.RemoveSentenceTypeAsync(id); //GetSentenceManageListAsync
                _logger.LogInformation("刪除句子類型");

                if (response)
                {
                    _logger.LogInformation("刪除句子類型");
                    return Ok(response);
                }
                else
                {
                    return BadRequest("找不到資料刪除");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(AddSentenceType));
                return ServerError500();
            }
        }

        /// <summary>
        /// 刪除句子
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpDelete]
        [Route("removesentence")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveSentence(int id)
        {
            try
            {
                var response = await _domain.RemoveSentenceAsync(id); //GetSentenceManageListAsync
                _logger.LogInformation("刪除句子");

                if (response)
                {
                    _logger.LogInformation("刪除句子");
                    return Ok(response);
                }
                else
                {
                    return BadRequest("找不到資料刪除");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(RemoveSentence));
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
                if (request.mp3File == null || request.mp3File.Length == 0)
                {
                    return BadRequest("No file provided.");
                }
                if (request.mp3File.ContentType != "audio/mpeg")
                {
                    return BadRequest("The provided file is not an MP3.");
                }

                var response = await _domain.CreateSentenceManageAsync(request); //GetSentenceManageListAsync
                if (response)
                {
                    _logger.LogInformation("檔案名稱已重複");
                    return Ok(response);
                }
                else
                {
                    return BadRequest("fileName error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(CreateSentenceManage));
                return ServerError500();
            }
        }

        /// <summary>
        /// 修改句子
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpPost]
        [Route("updatesentence")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSentenceManage([FromForm] SentenceManageEditRequest request)
        {
            try
            {
                var response = await _domain.UpdateSentenceManageAsync(request); //GetSentenceManageListAsync
                if (response)
                {
                    _logger.LogInformation("檔案名稱已重複");
                    return Ok(response);
                }
                else
                {
                    return BadRequest("fileName error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(CreateSentenceManage));
                return ServerError500();
            }
        }


    }
}