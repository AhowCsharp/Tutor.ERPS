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
using NPOI.POIFS.Crypt.Dsig;

namespace TUTOR.Biz.Domain.API
{
    public class SentenceManageDomain : ISentenceManageDomain
    {
        private readonly ISentenceManageRepository _sentenceManageRepository;
        private readonly IStudentAnswerLogRepository _studentAnswerLogRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        private readonly UserService _userService;

        public SentenceManageDomain(ISentenceManageRepository sentenceManageRepository, IMemberRepository memberRepository, IHttpContextAccessor httpContextAccessor, UserService userService, 
            IMapper mapper, IStudentAnswerLogRepository studentAnswerLogRepository)
        {
            _sentenceManageRepository = sentenceManageRepository;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _memberRepository = memberRepository;
            _studentAnswerLogRepository = studentAnswerLogRepository;
            _mapper = mapper;
        }

        public async Task<SentenceManageResponse> GetSentenceManageListAsync()
        {
            var data = await _sentenceManageRepository.GetSentenceManageListAsync();
            var types = await _sentenceManageRepository.GetSentenceTypeListAsync();
            data = data.ToList();
            foreach (var item in data)
            {
                var type = types.FirstOrDefault(t => t.id == item.questionTypeId);
                if (type != null)
                {
                    item.typeName = type.type;
                }
            }
            return new SentenceManageResponse(data);
        }

        public async Task<SentenceResponse> GetSentenceListAsync(string typeName,string account,string name)
        {
            var student = await _memberRepository.GetMemberAsync(account, name);
            var types = await _sentenceManageRepository.GetSentenceTypeListAsync();
            var type = types.FirstOrDefault(t => t.type == typeName);
            //var subjects = new List<Subject>();
            if (type != null)
            {
                var lastTime = await _studentAnswerLogRepository.GetLastStudentAnswerLogAsync(student.id, type.type);
                int? questionNumber = lastTime?.questionNumber != null ? Convert.ToInt32(lastTime.questionNumber) : null;
                var data = await _sentenceManageRepository.GetSentenceListAsync(type.id, questionNumber);
                data = data.ToList();

                foreach (var item in data)
                {
                    var log = await _studentAnswerLogRepository.GetStudentAnswerLogAsync(student.id, type.type, item.questionNum.ToString());
                    var subject = new Subject();
                    item.typeName = type.type;
                    subject.questionSentence = item.questionSentence;
                    subject.mp3FileName = item.mp3FileName;
                    subject.mp3FileUrl = item.mp3FileUrl;
                    subject.questionNum = item.questionNum;
                    subject.typeName = type.type;
                    subject.preAnswer = log != null? log.answerLog : null;
                    item.subject = subject;
                    if(log != null)
                    {
                        item.isPassing = log.status == 1 ? true : false; 
                    }
                    //subjects.Add(subject);
                }

                return new SentenceResponse(data);
            }
            else 
            {
                return new SentenceResponse(null);
            }
        }


        public async Task<bool> CreateSentenceTypeAsync(SentenceTypeRequest sentenceTypeRequest)
        {
            var dto = _mapper.Map<SentenceTypeDTO>(sentenceTypeRequest);
            var isPass = await _sentenceManageRepository.CreateTypeAsync(dto);

            return isPass;
        }

        public async Task<bool> RemoveSentenceTypeAsync(int id)
        {
            var isPass = await _sentenceManageRepository.RemoveTypeAsync(id);

            return isPass;
        }

        public async Task<bool> RemoveSentenceAsync(int id)
        {
            bool isPass;
            try
            {
                // 获取数据库中的现有实体
                var existingEntity = await _sentenceManageRepository.GetAsync(id);

                if (existingEntity == null)
                {                
                    return false;
                }
                else 
                {
                    isPass =await _sentenceManageRepository.RemoveSentenceAsync(id);
                    var fileName = existingEntity.mp3FileName;

                    // 获取文件存储目录
                    var rootDirectory = Directory.GetCurrentDirectory();
                    var uploadsDirectory = Path.Combine(rootDirectory, "wwwroot", "uploads");

                    // 定义要删除的文件的完整路径
                    var filePath = Path.Combine(uploadsDirectory, fileName);

                    // 删除文件
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                
                return isPass;
            }
            catch (Exception ex)
            {
                // 处理或记录异常
                return false;
            }
        }

        public async Task<bool> CreateSentenceManageAsync(SentenceManageRequest sentenceManageRequest)
        {
            var rootDirectory = Directory.GetCurrentDirectory();
            var uploadsDirectory = Path.Combine(rootDirectory, "wwwroot", "uploads");  // 指向wwwroot/uploads

            // 檢查 "uploads" 資料夾是否存在，如果不存在則建立它
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            // 定義檔案的完整儲存路徑
            var filePath = Path.Combine(uploadsDirectory, sentenceManageRequest.mp3File.FileName);

            var dto = _mapper.Map<SentenceManageDTO>(sentenceManageRequest);
            var types = await _sentenceManageRepository.GetSentenceTypeListAsync();
            var type = types.FirstOrDefault(x => x.type == dto.typeName);

            dto.questionTypeId = type != null ? type.id : null;

            // 儲存相對路徑到資料庫
            dto.mp3FileUrl = $"/uploads/{sentenceManageRequest.mp3File.FileName}";

            dto.mp3FileName = sentenceManageRequest.mp3File.FileName;
            dto.studyLevel = type != null ? type.studyLevel : 0;

            var isPass = await _sentenceManageRepository.CreateSentenceAsync(dto);

            // 儲存檔案
            if (isPass)
            {
                await using var fileStream = new FileStream(filePath, FileMode.Create);
                await sentenceManageRequest.mp3File.CopyToAsync(fileStream);
            }

            return isPass;
        }

        public async Task<bool> UpdateSentenceManageAsync(SentenceManageEditRequest sentenceManageRequest)
        {
            var rootDirectory = Directory.GetCurrentDirectory();
            var uploadsDirectory = Path.Combine(rootDirectory, "wwwroot", "uploads");

            // 检查 "uploads" 文件夹是否存在，如果不存在则创建它
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }
            var exsit = await _sentenceManageRepository.GetAsync(sentenceManageRequest.id);
            var types = await _sentenceManageRepository.GetSentenceTypeListAsync();
            var type = types.FirstOrDefault(x => x.type == sentenceManageRequest.typeName);
            bool isPass;
            // 如果有新文件，删除旧文件
            if (sentenceManageRequest.mp3File != null)
            {
                if (sentenceManageRequest.mp3File.ContentType != "audio/mpeg")
                {
                    return false;
                }
                var oldFilePath = Path.Combine(uploadsDirectory, exsit.mp3FileName);  // 替换 "OldFileName.mp3" 为从数据库或其他地方获取的旧文件名
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
                var newFilePath = Path.Combine(uploadsDirectory, sentenceManageRequest.mp3File.FileName);

                exsit.questionTypeId = type != null ? type.id : null;

                // 儲存相對路徑到資料庫
                exsit.mp3FileUrl = $"/uploads/{sentenceManageRequest.mp3File.FileName}";

                exsit.mp3FileName = sentenceManageRequest.mp3File.FileName;
                exsit.studyLevel = type != null ? type.studyLevel : 0;
                exsit.questionSentence = sentenceManageRequest.questionSentence;
                exsit.questionSentenceChinese = sentenceManageRequest.questionSentenceChinese;
                exsit.questionAnswer = sentenceManageRequest.questionAnswer;
                isPass = await _sentenceManageRepository.UpdateSentenceAsync(exsit);
                if (isPass)
                {
                    await using var fileStream = new FileStream(newFilePath, FileMode.Create);
                    await sentenceManageRequest.mp3File.CopyToAsync(fileStream);
                }
                return isPass;
            }
            exsit.questionTypeId = type != null ? type.id : null;
            exsit.questionSentence = sentenceManageRequest.questionSentence;
            exsit.questionSentenceChinese = sentenceManageRequest.questionSentenceChinese;
            exsit.questionAnswer = sentenceManageRequest.questionAnswer;
            exsit.studyLevel = type != null ? type.studyLevel : 0;
            isPass = await _sentenceManageRepository.UpdateSentenceAsync(exsit);


            return isPass;
        }



        public async Task<SentenceTypeResponse> GetSentenceTypeListAsync()
        {
            var data = await _sentenceManageRepository.GetSentenceTypeListAsync();

            return new SentenceTypeResponse(data);
        }
    }
}