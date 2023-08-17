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

namespace TUTOR.Biz.Domain.API
{
    public class StudentAnswerLogDomain : IStudentAnswerLogDomain
    {
        private readonly IStudentAnswerLogRepository _studentAnswerLogRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly UserService _userService;

        public StudentAnswerLogDomain(IStudentAnswerLogRepository studentAnswerLogRepository, IHttpContextAccessor httpContextAccessor, UserService userService)
        {
            _studentAnswerLogRepository = studentAnswerLogRepository;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<StudentAnswerLogResponse> GetStudentAnswerLogsAsync(int id)
        {
            var data = await _studentAnswerLogRepository.GetStudentAnswerLogsAsync(id);

            return new StudentAnswerLogResponse(data);
        }
    }
}