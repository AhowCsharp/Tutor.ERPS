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
using TUTOR.Biz.Models.Responses.SentenceManage;
using NPOI.SS.Formula.Functions;
using TUTOR.Biz.Services;
using TUTOR.Biz.SeedWork;
using TUTOR.Biz.Models.Requests;
using MathNet.Numerics.Distributions;
using System.Data.Entity;
using Org.BouncyCastle.Asn1.Ocsp;
using TUTOR.Biz.Models.Responses.SentenceType;
using AutoMapper;

namespace TUTOR.Biz.Domain.API
{
    public class SentenceManageDomain : ISentenceManageDomain
    {
        private readonly ISentenceManageRepository _sentenceManageRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        private readonly UserService _userService;

        public SentenceManageDomain(ISentenceManageRepository sentenceManageRepository, IHttpContextAccessor httpContextAccessor, UserService userService, IMapper mapper)
        {
            _sentenceManageRepository = sentenceManageRepository;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<SentenceManageResponse> GetSentenceManageListAsync()
        {
            var data = await _sentenceManageRepository.GetSentenceManageListAsync();
            var types = await _sentenceManageRepository.GetSentenceTypeListAsync();
            var test = await _sentenceManageRepository.GetAsync(1);
            test.TypeName = "test";
            var x = test;
            foreach (var item in data)
            {
                var type = types.FirstOrDefault(t => t.Id == item.QuestionTypeId);
                if (type != null)
                {
                    item.TypeName = type.Type;
                }
            }

            return new SentenceManageResponse(data);
        }

        public async Task<bool> CreateSentenceManageAsync(SentenceManageRequest sentenceManageRequest)
        {
            var rootDirectory = Directory.GetCurrentDirectory();
            var uploadsDirectory = Path.Combine(rootDirectory, "uploads");

            // 檢查 "uploads" 資料夾是否存在，如果不存在則建立它
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            // 定義檔案的完整儲存路徑
            var filePath = Path.Combine(uploadsDirectory, sentenceManageRequest.Mp3File.FileName);

            var dto = _mapper.Map<SentenceManageDTO>(sentenceManageRequest);
            var types = await _sentenceManageRepository.GetSentenceTypeListAsync();
            var type = types.FirstOrDefault(x => x.Type == dto.TypeName);
            dto.QuestionTypeId = type != null ? type.Id : null;
            dto.Mp3FileUrl = filePath;
            dto.Mp3FileName = sentenceManageRequest.Mp3File.FileName;

            // 儲存檔案
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await sentenceManageRequest.Mp3File.CopyToAsync(fileStream);

            var isPass = await _sentenceManageRepository.CreateSentenceAsync(dto);
            return isPass;
        }

        public async Task<SentenceTypeResponse> GetSentenceTypeListAsync()
        {
            var data = await _sentenceManageRepository.GetSentenceTypeListAsync();

            return new SentenceTypeResponse(data);
        }
    }
}