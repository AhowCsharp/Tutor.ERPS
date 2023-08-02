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

namespace TUTOR.Biz.Domain.API
{
    public class MemberDomain : IMemberDomain
    {
        private readonly IMemberRepository MemberRepo;

        private readonly IHttpContextAccessor httpContextAccessor;

        public MemberDomain(IMemberRepository MemberRepo, IHttpContextAccessor httpContextAccessor)
        {
            this.MemberRepo = MemberRepo;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<StatusResponse> ImportFromExcel(IFormFile file)
        {
            try
            {
                var memberDTOs = new List<MemberDTO>();

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0; // reset the position to the beginning of the stream.

                    var workbook = new XSSFWorkbook(stream); // using NPOI.XSSF.UserModel;
                    var sheet = workbook.GetSheetAt(0); // Assuming the data is in the first sheet

                    for (int row = 1; row <= sheet.LastRowNum; row++) // Skip header row (row 0)
                    {
                        var rowObject = sheet.GetRow(row);
                        var dto = new MemberDTO
                        {
                            Name = rowObject.GetCell(0)?.ToString(),
                            Account = rowObject.GetCell(1)?.ToString(),
                            Password = rowObject.GetCell(2)?.ToString(),
                            Email = rowObject.GetCell(3)?.ToString(),
                            StudyLevel = int.TryParse(rowObject.GetCell(4)?.ToString(), out int studyLevel) ? studyLevel : 0,
                            Status = int.TryParse(rowObject.GetCell(5)?.ToString(), out int status) ? status : 0,
                            StartDate = DateTime.TryParse(rowObject.GetCell(6)?.ToString(), out DateTime startDate) ? startDate : DateTime.Now,
                            EndDate = DateTime.TryParse(rowObject.GetCell(7)?.ToString(), out DateTime endDate) ? endDate : DateTime.Now,
                            BeDeleted = int.TryParse(rowObject.GetCell(8)?.ToString(), out int beDeleted) ? beDeleted : 0,
                            CreateDate = DateTime.Now
                        };

                        memberDTOs.Add(dto);
                    }
                }
                return await MemberRepo.InsertExcelDatas(memberDTOs);
            }
            catch (Exception ex)
            {
                // 在這裡記錄異常詳情
                var errMsg = new List<string>();
                errMsg.Add(ex.Message);
                var res = new StatusResponse(errMsg, HttpStatusCode.BadRequest);
                return res;
            }
        }

        public MemberResponse GetList(int page)
        {
            var dtos = MemberRepo.GetList(page);
            if (dtos == null || !dtos.Any())
            {
                return new MemberResponse(null);
            }
            else
            {
                var list = dtos.Select(x => new Member
                {
                    Account = x.Account,
                    Email = x.Email,
                    Status = x.Status,
                    StudyLevel = x.StudyLevel,
                    BeDeleted = x.BeDeleted,
                    Name = x.Name,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                });

                return new MemberResponse(list);
            }
        }
    }
}