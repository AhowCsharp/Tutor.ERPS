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
    [Route("member")]
    [ApiController]
    [ApTokenAuth]
    public class MemberController : BaseController
    {
        private readonly ILogger<MemberController> _logger;
        private readonly MemberDomain _domain;

        public MemberController(ILogger<MemberController> logger, MemberDomain domain)
        {
            _logger = logger;
            _domain = domain;
        }

        /// <summary>
        /// 取得有效學生清單
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpGet]
        [Route("student")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMemberList()
        {
            try
            {
                var response = await _domain.GetMemberListAsync(); //GetMemberListAsync
                _logger.LogInformation("取得有效會員清單");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(GetMemberList));
                return ServerError500();
            }
        }

        /// <summary>
        /// 儲存修改學生資訊
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpPatch]
        [Route("{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveStudentInfo(int studentId, MemberRequest request)
        {
            try
            {
                var response = await _domain.SaveStudentInfo(studentId, request);
                _logger.LogInformation("儲存會員");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(SaveStudentInfo));
                return ServerError500();
            }
        }

        /// <summary>
        /// 批次新增學生資訊
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">請求錯誤</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpPost]
        [Route("studentInfos")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadStudentsInfo([FromForm] IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    _logger.LogWarning("沒有提供有效的Excel文件");
                    return BadRequest(ResultError.ERR_TOKEN);
                }

                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0; // 重置流的位置

                // 轉換並新增學生資訊
                var response = await _domain.AddStudentsFromExcel(stream);
                _logger.LogInformation("批次新增學生資訊成功");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(UploadStudentsInfo));
                return ServerError500();
            }
        }

        /// <summary>
        /// 新增學生資訊
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">請求錯誤</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpPost]
        [Route("studentInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddStudentInfo(MemberRequest request)
        {
            try
            {
                var response = await _domain.AddStudentInfo(request);
                _logger.LogInformation("新增學生資訊成功");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(AddStudentInfo));
                return ServerError500();
            }
        }

        /// <summary>
        /// 刪除學生資訊
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">請求錯誤</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpPost]
        [Route("studentStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteStudentInfo(int studentId)
        {
            try
            {
                var response = await _domain.DeleteStudentInfo(studentId);
                _logger.LogInformation("刪除學生資訊成功");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(DeleteStudentInfo));
                return ServerError500();
            }
        }
    }
}