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
using TUTOR.Biz.Models.Responses.StudentAnswerLog;
using NPOI.SS.Formula.Functions;
using TUTOR.Biz.Services;
using TUTOR.Biz.SeedWork;
using TUTOR.Biz.Models.Requests;
using MathNet.Numerics.Distributions;
using System.Data.Entity;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Text.RegularExpressions;
using System.Globalization;

namespace TUTOR.Biz.Domain.API
{
    public class StudentAnswerLogDomain : IStudentAnswerLogDomain
    {
        private readonly IStudentAnswerLogRepository _studentAnswerLogRepository;

        private readonly ISentenceManageRepository _sentenceManageRepository;

        private readonly IMemberRepository _memberRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly UserService _userService;

        public StudentAnswerLogDomain(IStudentAnswerLogRepository studentAnswerLogRepository, 
            IHttpContextAccessor httpContextAccessor, UserService userService,
            ISentenceManageRepository sentenceManageRepository, IMemberRepository memberRepository)
        {
            _studentAnswerLogRepository = studentAnswerLogRepository;
            _sentenceManageRepository = sentenceManageRepository;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _memberRepository = memberRepository;
        }

        public async Task<StudentAnswerLogResponse> GetStudentAnswerLogsAsync(int id)
        {
            var data = await _studentAnswerLogRepository.GetStudentAnswerLogsAsync(id);

            return new StudentAnswerLogResponse(data);
        }

        public async Task<bool> VerifyAnswer(StudentAnswerRequest studentAnswer)
        {
            var typeDTO = await _sentenceManageRepository.GetSentenceTypeAsync(studentAnswer.questionType);
            var question = await _sentenceManageRepository.GetSentenceManageAsync(typeDTO.id, studentAnswer.questionNum);
            var student = await _memberRepository.GetMemberAsync(studentAnswer.account, studentAnswer.name);

          
            var answerList = Regex.Split(question.questionAnswer, @"\s+").ToList();
            var studentAnswerList = Regex.Split(studentAnswer.answer, @"\s+").ToList();
            var errorNum = CompareLists(answerList, studentAnswerList);
            var fullscore = 100;
            double eachError = Math.Round(100.0 / answerList.Count());

            bool isPass = fullscore - (errorNum * eachError) > 60;

            var answerLog = new StudentAnswerLogDTO();
            answerLog.studentId = student.id;
            answerLog.questionType = typeDTO.type;
            answerLog.questionNumber = question.questionNum.ToString();
            answerLog.answerLog = studentAnswer.answer;
            answerLog.costTime = studentAnswer.timeCost.ToString() + "秒";
            answerLog.status = isPass?1:0;
            answerLog.answerDate = DateTime.Now;
            
            var exsitLog = await _studentAnswerLogRepository.GetStudentAnswerLogAsync(student.id, typeDTO.type, question.questionNum.ToString());
            if (exsitLog != null)
            {
                await _studentAnswerLogRepository.DeleteAsync(exsitLog.id);
            }
            return await _studentAnswerLogRepository.AddStudentAnswerLogAsync(answerLog);
        }

        static int CompareLists(List<string> list1, List<string> list2)
        {
            int wrongCount = 0;

            // 找出 list2 中缺少的元素
            foreach (var item in list1)
            {
                if (!list2.Contains(item))
                {
                    wrongCount++;
                }
            }

            // 檢查 list2 中的元素是否在正確的順序中出現
            int index = 0;
            foreach (var item in list1)
            {
                if (list2.Contains(item))
                {
                    int newIndex = list2.IndexOf(item, index);
                    if (newIndex == -1)
                    {
                        wrongCount++;
                    }
                    else
                    {
                        index = newIndex + 1;
                    }
                }
            }

            return wrongCount;
        }

    }
}