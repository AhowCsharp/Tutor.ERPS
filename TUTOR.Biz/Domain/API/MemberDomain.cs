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
using TUTOR.Biz.Models.Responses.Member;
using NPOI.SS.Formula.Functions;
using TUTOR.Biz.Services;
using TUTOR.Biz.SeedWork;
using TUTOR.Biz.Models.Requests;
using MathNet.Numerics.Distributions;
using System.Data.Entity;
using Org.BouncyCastle.Asn1.Ocsp;
using AutoMapper;

namespace TUTOR.Biz.Domain.API
{
    public class MemberDomain : IMemberDomain
    {
        private readonly IMemberRepository _memberRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        private readonly UserService _userService;

        public MemberDomain(IMemberRepository memberRepository, IHttpContextAccessor httpContextAccessor, UserService userService, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<MemberResponse> GetMemberListAsync()
        {
            var data = await _memberRepository.GetMemberListAsync();

            return new MemberResponse(data);
        }

        public async Task<CommonResult> SaveStudentInfo(int studentId, MemberRequest request)
        {
            var result = new CommonResult();
            var exist = await _memberRepository.GetAsync(studentId);
            if (exist == null)
            {
                result.AddError(ResultError.ERR_STUDENT);
            }

            if (result.Errors.Any())
            {
                result.Result = false;

                return result;
            }

            exist.Creator = request.Creator;
            exist.Name = request.Name;
            exist.StartDate = request.StartDate;
            exist.EndDate = request.EndDate;
            exist.BeDeleted = request.BeDeleted;
            exist.Account = request.Account;
            exist.Status = request.Status;
            exist.Email = request.Email;
            exist.Password = request.Password;
            exist.Name = request.Name;
            exist.StudyLevel = request.StudyLevel;
            await _memberRepository.UpdateAsync(exist);

            result.Result = true;

            return result;
        }

        public async Task<CommonResult> AddStudentsFromExcel(Stream excelStream)
        {
            var result = new CommonResult();
            try
            {
                var students = new List<MemberDTO>();

                // 使用 NPOI 讀取 Excel 文件
                var workbook = new XSSFWorkbook(excelStream);
                var sheet = workbook.GetSheetAt(0); // 取得第一個工作表

                for (int row = 1; row <= sheet.LastRowNum; row++) // 從第二行開始讀取，假設第一行是標題
                {
                    var currentRow = sheet.GetRow(row);
                    if (currentRow == null) continue;

                    var member = new MemberDTO
                    {
                        Account = currentRow.GetCell(0)?.ToString(),
                        Email = currentRow.GetCell(1)?.ToString(),
                        Password = currentRow.GetCell(2)?.ToString(),
                        Status = int.Parse(currentRow.GetCell(3)?.ToString() ?? "0"),
                        StudyLevel = int.TryParse(currentRow.GetCell(4)?.ToString(), out var studyLevel) ? studyLevel : (int?)null,
                        BeDeleted = int.Parse(currentRow.GetCell(5)?.ToString() ?? "0"),
                        Creator = currentRow.GetCell(6)?.ToString(),
                        Editor = currentRow.GetCell(7)?.ToString(),
                        Name = currentRow.GetCell(8)?.ToString(),
                        Id = int.Parse(currentRow.GetCell(9)?.ToString() ?? "0"),
                        StartDate = DateTime.TryParse(currentRow.GetCell(10)?.ToString(), out var startDate) ? startDate : (DateTime?)null,
                        EndDate = DateTime.TryParse(currentRow.GetCell(11)?.ToString(), out var endDate) ? endDate : (DateTime?)null,
                        CreateDate = DateTime.TryParse(currentRow.GetCell(12)?.ToString(), out var createDate) ? createDate : (DateTime?)null,
                    };
                    students.Add(member);
                }

                await _memberRepository.AddStudentsFromExcel(students);
                result.Result = true;
                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
                result.Result = false;
                return result;
            }
        }

        public async Task<CommonResult> AddStudentInfo(MemberRequest request)
        {
            var result = new CommonResult();
            try
            {
                var memberDTO = new MemberDTO();
                memberDTO.Creator = request.Creator;
                memberDTO.Name = request.Name;
                memberDTO.StartDate = request.StartDate;
                memberDTO.EndDate = request.EndDate;
                memberDTO.BeDeleted = request.BeDeleted;
                memberDTO.Account = request.Account;
                memberDTO.Status = request.Status;
                memberDTO.Email = request.Email;
                memberDTO.Password = request.Password;
                memberDTO.Name = request.Name;
                memberDTO.StudyLevel = request.StudyLevel;
                await _memberRepository.InsertAsync(memberDTO);
                result.Result = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = false;
                return result;
            }
        }

        public async Task<CommonResult> DeleteStudentInfo(int studentId)
        {
            var result = new CommonResult();
            try
            {
                var data = await _memberRepository.GetAsync(studentId);
                if (data != null)
                {
                    data.BeDeleted = 1;
                    await _memberRepository.UpdateAsync(data);
                    result.Result = true;
                    return result;
                }
                else
                {
                    result.Result = false;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                return result;
            }
        }
    }
}