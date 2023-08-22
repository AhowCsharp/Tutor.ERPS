using TUTOR.Biz.Domain.API.Interface;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Extensions;
using TUTOR.Biz.Helpers;
using Microsoft.AspNetCore.Http;
using TUTOR.Biz.Repository_Interfaces;
using TUTOR.Biz.Models;
using NPOI.XSSF.UserModel;
using TUTOR.Biz.Models.Responses.Member;
using TUTOR.Biz.Models.Responses.StudentAnswerLog;
using NPOI.SS.Formula.Functions;
using TUTOR.Biz.Services;
using TUTOR.Biz.SeedWork;
using TUTOR.Biz.Models.Requests;
using AutoMapper;
using System.Globalization;

namespace TUTOR.Biz.Domain.API
{
    public class MemberDomain : IMemberDomain
    {
        private readonly IMemberRepository _memberRepository;

        private readonly IStudentAnswerLogRepository _studentAnswerLogRepository;


        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        private readonly UserService _userService;

        public MemberDomain(IMemberRepository memberRepository, IHttpContextAccessor httpContextAccessor,
            UserService userService, IMapper mapper, IStudentAnswerLogRepository studentAnswerLogRepository)
        {
            _memberRepository = memberRepository;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mapper = mapper;
            _studentAnswerLogRepository = studentAnswerLogRepository;
        }

        public async Task<MemberResponse> GetMemberListAsync()
        {
            var data = await _memberRepository.GetMemberListAsync();

            return new MemberResponse(data);
        }

        public async Task<bool> SaveStudentInfo(List<MemberRequest> request)
        {
            foreach(var item in request)
            {
                var exist = await _memberRepository.GetAsync(item.id);
                if (exist == null)
                {
                    return false;
                }
                var editItem = _mapper.Map<MemberDTO>(item);
                await _memberRepository.EditMember(editItem);
            }
            return true;

        }

        public async Task<bool> AddStudentsFromExcel(Stream excelStream)
        {
            var result = new CommonResult();
            try
            {
                var students = new List<MemberDTO>();

                // 使用 NPOI 讀取 Excel 文件
                var workbook = new XSSFWorkbook(excelStream);
                var sheet = workbook.GetSheetAt(0); // 取得第一個工作表
                string[] formats = { "yyyy/M/d", "yyyy/M/dd", "yyyy/MM/dd", "yyyy/MM/d" };
                DateTime parsedDate;

                for (int row = 1; row <= sheet.LastRowNum; row++) // 從第二行開始讀取，假設第一行是標題
                {
                    var currentRow = sheet.GetRow(row);
                    if (currentRow == null) continue;

                    var member = new MemberDTO
                    {
                        account = currentRow.GetCell(0)?.ToString(),
                        email = currentRow.GetCell(1)?.ToString(),
                        password = currentRow.GetCell(2)?.ToString(),
                        status = int.TryParse(currentRow.GetCell(3)?.ToString(), out var parsedStatus) ? parsedStatus : 0,
                        studyLevel = int.TryParse(currentRow.GetCell(4)?.ToString(), out var studyLevel) ? studyLevel : (int?)null,
                        beDeleted = int.TryParse(currentRow.GetCell(5)?.ToString(), out var beDeleted) ? beDeleted : 0,
                        creator = currentRow.GetCell(6)?.ToString(),
                        editor = currentRow.GetCell(7)?.ToString(),
                        name = currentRow.GetCell(8)?.ToString()
                    };
                    var x = currentRow.GetCell(9)?.ToString();
                    var y = currentRow.GetCell(10)?.ToString();
                    var z = currentRow.GetCell(11)?.ToString();

                    var start = currentRow.GetCell(9)?.ToString();
                    var end = currentRow.GetCell(10)?.ToString();
                    var create = currentRow.GetCell(11)?.ToString();
                    member.startDate = ParseCustomDateFormat(start);
                    member.endDate = ParseCustomDateFormat(end);
                    member.createDate = ParseCustomDateFormat(create);

                    students.Add(member);
                }

                return await _memberRepository.AddStudentsFromExcel(students);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
                return false;
            }
        }

        public static DateTime? ParseCustomDateFormat(string input)
        {
            try
            {
                if (input == null)
                { 
                    return null;
                }
                var monthMappings = new Dictionary<string, int>
                {
                    {"1月", 1},
                    {"2月", 2},
                    {"3月", 3},
                    {"4月", 4},
                    {"5月", 5},
                    {"6月", 6},
                    {"7月", 7},
                    {"8月", 8},
                    {"9月", 9},
                    {"10月", 10},
                    {"11月", 11},
                    {"12月", 12},
                };

                // Split by '-'
                var parts = input.Split('-');

                if (parts.Length != 3)
                {
                    return null; // Invalid format
                }

                int day = int.Parse(parts[0]);
                int month;
                if (!monthMappings.TryGetValue(parts[1], out month))
                {
                    return null; // Invalid month
                }
                int year = int.Parse(parts[2]);

                return new DateTime(year, month, day);
            }
            catch
            {
                return null; // Failed to parse
            }
        }

        public async Task<bool> AddStudentInfo(MemberRequest request)
        {

            try
            {
                var dto = _mapper.Map<MemberDTO>(request);
                await _memberRepository.InsertAsync(dto);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<StudentAnswerLogResponse> GetStudentAnswerLog(int id)
        { 
            var data = await _studentAnswerLogRepository.GetStudentAnswerLogsAsync(id);
            return new StudentAnswerLogResponse(data);
        }


        public async Task<bool> DeleteStudentInfo(int studentId)
        {
            var result = new CommonResult();
            try
            {
                var data = await _memberRepository.GetAsync(studentId);
                if (data != null)
                {
                    data.beDeleted = 1;
                    await _memberRepository.EditMember(data);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false; 
            }
        }
    }
}