using TUTOR.Biz.Domain.API.Interface;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Extensions;
using TUTOR.Biz.Helpers;
using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TUTOR.Biz.Repository_Interfaces;
using TUTOR.Biz.Models;
using NPOI.XSSF.UserModel;
using TUTOR.Biz.Models.Responses.AdminManage;
using TUTOR.Biz.Models.Responses.StudentAnswerLog;
using NPOI.SS.Formula.Functions;
using TUTOR.Biz.Services;
using TUTOR.Biz.SeedWork;
using TUTOR.Biz.Models.Requests;
using AutoMapper;
using System.Globalization;
using NPOI.POIFS.Crypt.Dsig;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace TUTOR.Biz.Domain.API
{
    public class AdminManageDomain 
    {
        private readonly IAdminManageRepository _AdminManageRepository;

        private readonly IStudentAnswerLogRepository _studentAnswerLogRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        private readonly UserService _userService;

        public AdminManageDomain(IAdminManageRepository AdminManageRepository, IHttpContextAccessor httpContextAccessor,
            UserService userService, IMapper mapper, IStudentAnswerLogRepository studentAnswerLogRepository)
        {
            _AdminManageRepository = AdminManageRepository;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mapper = mapper;
            _studentAnswerLogRepository = studentAnswerLogRepository;
        }

        public async Task<AdminManageResponse> GetAdminManageAsync(LoginRequest loginRequest)
        {
            var admin = await _AdminManageRepository.GetAdminManageAsync(loginRequest);
            if (admin != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("Name", admin.name),
                    new Claim("Account", admin.account),
                    new Claim("Status", admin.status.ToString())
                };
                var key = System.Text.Encoding.ASCII.GetBytes("MySecretKey12345"); // 替換為你自己的秘鑰

                // 建立 Header
                var header = new { alg = "HS256", typ = "JWT" };

                // 建立 Payload
                var payload = new Dictionary<string, object>
                {
                    { "Name", admin.name },
                    { "Account", admin.account },
                    { "Status", admin.status.ToString() },
                    { "exp", (int)(DateTime.UtcNow.AddHours(20) - new DateTime(1970, 1, 1)).TotalSeconds }
                };

                string headerString = JsonConvert.SerializeObject(header);
                string payloadString = JsonConvert.SerializeObject(payload);

                // Base64 編碼 Header 和 Payload
                string headerBase64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(headerString));
                string payloadBase64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(payloadString));

                // 生成要簽名的字符串
                string signatureString = headerBase64 + "." + payloadBase64;

                // 生成簽名
                using (HMACSHA256 hmac = new HMACSHA256(key))
                {
                    byte[] signatureBytes = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(signatureString));
                    string signatureBase64 = Convert.ToBase64String(signatureBytes);

                    // 組裝最終的 JWT token 字串
                    string tokenString = headerBase64 + "." + payloadBase64 + "." + signatureBase64;
                    admin.jwttoken = tokenString;
                }

                return new AdminManageResponse(admin, null);
            }
            else
            { 
                var student = await _AdminManageRepository.GetMemberAsync(loginRequest);
                return new AdminManageResponse(null, student);
            }

            
        }

    }
}