using TUTOR.Biz.Domain.API.Interface;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Extensions;
using TUTOR.Biz.Helpers;
using Microsoft.AspNetCore.Http;
using TUTOR.Biz.Repository_Interfaces;
using TUTOR.Biz.Models;
using NPOI.XSSF.UserModel;
using NPOI.SS.Formula.PTG;
using System.Net;
using TUTOR.Biz.Models.Responses.Game;
using NPOI.SS.Formula.Functions;
using TUTOR.Biz.Services;
using TUTOR.Biz.SeedWork;
using TUTOR.Biz.Models.Requests;
using MathNet.Numerics.Distributions;
using System.Data.Entity;
using Org.BouncyCastle.Asn1.Ocsp;
using TUTOR.Biz.Models.Responses.SentenceType;
using AutoMapper;
using TUTOR.Biz.Models.Responses.SentenceManage;
using NPOI.SS.UserModel;

namespace TUTOR.Biz.Domain.API
{
    public class GameDomain
    {
        private readonly IGameRepository _gameRepository;

        private readonly IGamerWordsLogRepository _gamerWordsLogRepository;
        private readonly IErrorWordLogRepository _errorWordLogRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        private readonly UserService _userService;

        public GameDomain(IGameRepository sentenceManageRepository, IHttpContextAccessor httpContextAccessor, UserService userService, IMapper mapper,
            IGamerWordsLogRepository gamerWordsLogRepository, IErrorWordLogRepository errorWordLogRepository)
        {
            _gameRepository = sentenceManageRepository;
            _httpContextAccessor = httpContextAccessor;
            _gamerWordsLogRepository = gamerWordsLogRepository;
            _userService = userService;
            _mapper = mapper;
            _errorWordLogRepository = errorWordLogRepository;
        }

        public async Task<GameResponse> GetGameWordsListAsync(string gamer, int level)
        {
            var wordsLog = await _gamerWordsLogRepository.GetGamerWordsLogsAsync(level, gamer);
            var errWordsLog = await _errorWordLogRepository.GetErrorWordLogsAsync(level, gamer);

            var exsitWordsList = new List<string>();
            var errWordsList = new List<string>();
            foreach (var word in wordsLog)
            {
                var singleArr = word.Words.Trim('[', ']').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < singleArr.Length; i++)
                {
                    exsitWordsList.Add(singleArr[i].Trim('"'));
                }
            }

            foreach (var word in errWordsLog)
            {
                errWordsList.Add(word.ErrorWord);
            }

            var gameWords = await _gameRepository.GetGameListAsync(level, exsitWordsList, errWordsList);
            return new GameResponse(gameWords);
        }

        public async Task<bool> CreateGameWordsMp3Aync(List<IFormFile> mp3Files)
        {
            try
            {
                var rootDirectory = Directory.GetCurrentDirectory();
                var uploadsDirectory = Path.Combine(rootDirectory, "wwwroot", "gameUploads");

                if (!Directory.Exists(uploadsDirectory))
                {
                    Directory.CreateDirectory(uploadsDirectory);
                }

                // 只取得一次 wordInfos
                var wordInfos = await _gameRepository.GetGameListAsync();

                foreach (var mp3 in mp3Files)
                {
                    var filePath = Path.Combine(uploadsDirectory, mp3.FileName);

                    var wordInfo = wordInfos.FirstOrDefault(w => w.Word == mp3.FileName);
                    if (wordInfo != null)
                    {
                        wordInfo.Mp3Url = $"/gameUploads/{mp3.FileName}";

                        // 更新資料庫
                        var isUpdate = await _gameRepository.UpdateGameMp3UrlAsync(wordInfo);
                        if (isUpdate)
                        {
                            await using var fileStream = new FileStream(filePath, FileMode.Create);
                            await mp3.CopyToAsync(fileStream);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // 最好是將錯誤資訊記錄下來，例如使用 log
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> ImportGameWordsAsync(IFormFile excelFile)
        {
            try
            {
                using var stream = new MemoryStream();
                await excelFile.CopyToAsync(stream);
                stream.Position = 0;

                IWorkbook workbook = new XSSFWorkbook(stream);
                ISheet sheet = workbook.GetSheetAt(0); //第一個工作表

                List<GameWordsDTO> gameWords = new List<GameWordsDTO>();
                for (int row = 1; row <= sheet.LastRowNum; row++) //從第二行開始，因為第一行是標題
                {
                    IRow dataRow = sheet.GetRow(row);
                    if (dataRow == null) continue;

                    string wordCell = dataRow.GetCell(0)?.ToString();
                    ICell hardLevelCell = dataRow.GetCell(1);

                    // 如果 wordCell 為 null 或 hardLevelCell 無法轉換為整數，則跳過這一行
                    if (string.IsNullOrEmpty(wordCell) ||
                        hardLevelCell == null ||
                        !int.TryParse(hardLevelCell.ToString(), out int hardLevel))
                    {
                        continue;
                    }
                    var exist = await _gameRepository.GetRepeatWordAsync(wordCell);
                    if (exist != null)
                    {
                        continue;
                    }
                    GameWordsDTO gameWord = new GameWordsDTO
                    {
                        Word = wordCell,
                        HardLevel = hardLevel
                    };

                    gameWords.Add(gameWord);
                }
                return await _gameRepository.UpdateGameWordsAsync(gameWords);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}