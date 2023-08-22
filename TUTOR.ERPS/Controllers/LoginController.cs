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
    [Route("login")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly ILogger<LoginController> _logger;
        private readonly AdminManageDomain _domain;

        public LoginController(ILogger<LoginController> logger, AdminManageDomain domain)
        {
            _logger = logger;
            _domain = domain;
        }

        /// <summary>
        /// 取得有效權限
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpPost]
        [Route("verify")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuth(LoginRequest loginRequest)
        {
            try
            {
                var response = await _domain.GetAdminManageAsync(loginRequest); //GetMemberListAsync
                if (response.adminManageDTO == null && response.memberDTO == null)
                {
                    return BadRequest("帳密錯誤");
                }
                _logger.LogInformation("取得有效權限");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(GetAuth));
                return ServerError500();
            }
        }

    }
}