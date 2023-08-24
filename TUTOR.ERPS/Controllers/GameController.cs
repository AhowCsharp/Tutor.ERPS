using TUTOR.ERPS.API.Infa;
using TUTOR.ERPS.API.Infa.Attribute;
using TUTOR.Biz.Domain.API;
using TUTOR.Biz.Models;
using TUTOR.Biz.Models.Responses.Game;
using TUTOR.Biz.Utility;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using TUTOR.Biz.Models.Requests;

namespace TUTOR.ERPS.API
{
    /// <response code="401">驗證失敗</response>
    [Route("game")]
    [ApiController]
    [ApTokenAuth]
    public class GameController : BaseController
    {
        private readonly ILogger<GameController> _logger;
        private readonly GameDomain _domain;

        public GameController(ILogger<GameController> logger, GameDomain domain)
        {
            _logger = logger;
            _domain = domain;
        }

        /// <summary>
        /// 取得單字清單
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpGet]
        [Route("words")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGameWordsList(string? gamer, int? level)
        {
            try
            {
                var response = await _domain.GetGameWordsListAsync(gamer, level);
                _logger.LogInformation("取得單字清單");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(GetGameWordsList));
                return ServerError500();
            }
        }

        /// <summary>
        /// 新增單字音檔
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpPost]
        [Route("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGameMp3([FromForm] List<IFormFile> mp3Files)
        {
            try
            {
                if (mp3Files == null || mp3Files.Count() == 0)
                {
                    return BadRequest("No file provided.");
                }

                var response = await _domain.CreateGameWordsMp3Aync(mp3Files);
                if (response)
                {
                    _logger.LogInformation("檔案上傳成功");
                    return Ok(response);
                }
                else
                {
                    return BadRequest("fileName error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(CreateGameMp3));
                return ServerError500();
            }
        }

        [HttpPost]
        [Route("words")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGameWords([FromForm] IFormFile excelFile)
        {
            try
            {
                if (excelFile == null || excelFile.Length == 0)
                {
                    return BadRequest("No Excel file provided.");
                }

                var response = await _domain.ImportGameWordsAsync(excelFile);

                if (response)
                {
                    _logger.LogInformation("Successfully created game words.");
                    return Ok(response);
                }
                else
                {
                    return BadRequest("An error occurred while processing the Excel file.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(CreateGameWords));
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal error occurred.");
            }
        }

        [HttpPatch]
        [Route("editword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditGameWords(List<GameWordRequest> gameWordRequests)
        {
            try
            {


                var response = await _domain.EditGameWordsAsync(gameWordRequests);

                if (response)
                {
                    _logger.LogInformation("Successfully edited game words.");
                    return Ok(response);
                }
                else
                {
                    return BadRequest("An error occurred while processing the edited.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(EditGameWords));
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal error occurred.");
            }
        }

        [HttpDelete]
        [Route("deleteword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGameWords(int id)
        {
            try
            {
                var response = await _domain.DeleteGameWords(id);

                if (response)
                {
                    _logger.LogInformation("Successfully deleted game words.");
                    return Ok(response);
                }
                else
                {
                    return BadRequest("An error occurred while processing the edited.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(EditGameWords));
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal error occurred.");
            }
        }

        /// <summary>
        /// 記錄錯誤單字
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="403">無此權限</response>
        /// <response code="500">內部錯誤</response>
        [HttpPost]
        [Route("errorlog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateErrWord(string errWord,int level,string name)
        {
            try
            {

                var response = await _domain.CreateErrword(errWord,level,name); //GetGameListAsync
                if (response)
                {
                    _logger.LogInformation("記錄錯誤");
                    return Ok(response);
                }
                else
                {
                    return BadRequest("fileName error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(CreateErrWord));
                return ServerError500();
            }
        }
    }
}