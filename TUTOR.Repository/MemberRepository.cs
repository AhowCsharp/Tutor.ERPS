using Microsoft.AspNetCore.Components.Forms;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Repository_Interfaces;
using TUTOR.Repository.EF;
using TUTOR.Biz.Models.Requests;
using System.Net;
using Microsoft.AspNetCore.Http;
using NPOI.XSSF.UserModel;
using TUTOR.Biz.Models;

namespace TUTOR.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly TUTORERPSContext context;

        public MemberRepository(TUTORERPSContext context)
        {
            this.context = context;
        }

        public IEnumerable<MemberDTO>? GetList(int page)
        {
            var data = context.Member
                .Skip((page - 1) * 20) //一次20筆
                .Take(20)
                .Select(x => new MemberDTO
                {
                    Account = x.Account,
                    Email = x.Email,
                    Password = x.Password,
                    Status = x.Status,
                    StudyLevel = x.StudyLevel,
                    BeDeleted = x.BeDeleted,
                    Creator = x.Creator,
                    Editor = x.Editor,
                    Id = x.Id,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Name = x.Name,
                });

            if (data.Any())
            {
                return data.ToList();
            }
            else
            {
                return null;
            }
        }

        public MemberDTO? Get(MemberRequest memberRequest)
        {
            var query = context.Member.AsQueryable();

            if (!string.IsNullOrEmpty(memberRequest.Account))
            {
                query = query.Where(x => x.Account == memberRequest.Account);
            }

            if (!string.IsNullOrEmpty(memberRequest.Email))
            {
                query = query.Where(x => x.Email == memberRequest.Email);
            }

            if (!string.IsNullOrEmpty(memberRequest.Name))
            {
                query = query.Where(x => x.Name == memberRequest.Name);
            }

            if (memberRequest.Id.HasValue)
            {
                query = query.Where(x => x.Id == memberRequest.Id);
            }
            var member = query.Select(x => new MemberDTO
            {
                Account = x.Account,
                Email = x.Email,
                Password = x.Password,
                Status = x.Status,
                StudyLevel = x.StudyLevel,
                BeDeleted = x.BeDeleted,
                Creator = x.Creator,
                Editor = x.Editor,
                Id = x.Id,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Name = x.Name,
            }).FirstOrDefault();

            return member;
        }

        public async Task<HttpStatusCode> EditOrCreate(MemberRequest memberRequest)
        {
            if (memberRequest.Id.HasValue)
            {
                var exist = context.Member.FirstOrDefault(x => x.Id == memberRequest.Id);
                if (exist != null)
                {
                    exist.StartDate = memberRequest.StartDate != null ? memberRequest.StartDate.Value : exist.StartDate;
                    exist.EndDate = memberRequest.EndDate != null ? memberRequest.EndDate.Value : exist.EndDate;
                    exist.BeDeleted = memberRequest.BeDeleted != null ? memberRequest.BeDeleted.Value : exist.BeDeleted;
                    exist.Status = memberRequest.Status != null ? memberRequest.Status.Value : exist.Status;
                    exist.Account = memberRequest.Account != null ? memberRequest.Account : exist.Account;
                    exist.Password = memberRequest.Password != null ? memberRequest.Password : exist.Password;
                    exist.EndDate = memberRequest.EndDate != null ? memberRequest.EndDate.Value : exist.EndDate;
                    exist.Email = memberRequest.Email != null ? memberRequest.Email : exist.Email;
                    exist.Name = memberRequest.Name != null ? memberRequest.Name : exist.Name;
                    exist.StudyLevel = memberRequest.StudyLevel != null ? memberRequest.StudyLevel : exist.StudyLevel;
                    await context.SaveChangesAsync();
                    return HttpStatusCode.OK;
                }
                return HttpStatusCode.BadRequest;
            }
            else
            {
                var main = new Member();
                try
                {
                    main.Name = memberRequest.Name;
                    main.StudyLevel = memberRequest.StudyLevel;
                    main.Account = memberRequest.Account;
                    main.Password = memberRequest.Password;
                    main.Status = memberRequest.Status != null ? memberRequest.Status.Value : 0;
                    main.Email = memberRequest.Email;
                    main.CreateDate = DateTime.Now;
                    main.EndDate = memberRequest.EndDate != null ? memberRequest.EndDate.Value : DateTime.Now;
                    main.StartDate = DateTime.Now;
                    main.BeDeleted = memberRequest.BeDeleted != null ? memberRequest.BeDeleted.Value : 0;
                    await context.AddAsync(main);
                    return HttpStatusCode.OK;
                }
                catch (Exception ex)
                {
                    return HttpStatusCode.BadRequest;
                }
            }
        }

        public async Task<StatusResponse> InsertExcelDatas(IEnumerable<MemberDTO> memberDTOs)
        {
            var errMsg = new List<string>();
            try
            {
                foreach (var dto in memberDTOs)
                {
                    var member = new Member
                    {
                        Name = dto.Name,
                        StudyLevel = dto.StudyLevel,
                        Account = dto.Account,
                        Password = dto.Password,
                        Status = dto.Status,
                        Email = dto.Email,
                        CreateDate = DateTime.Now,
                        StartDate = dto.StartDate == null ? DateTime.Now : dto.StartDate.Value,
                        EndDate = dto.EndDate == null ? DateTime.Now : dto.EndDate.Value,
                        BeDeleted = dto.BeDeleted
                    };

                    await context.Member.AddAsync(member);
                }

                await context.SaveChangesAsync();
                var res = new StatusResponse(errMsg, HttpStatusCode.OK);
                return res;
            }
            catch (Exception ex)
            {
                // Log your exception
                errMsg.Add(ex.Message);
                var res = new StatusResponse(errMsg, HttpStatusCode.BadRequest);
                return res;
            }
        }

        public async Task<HttpStatusCode> Delete(MemberRequest memberRequest)
        {
            if (memberRequest.Id.HasValue)
            {
                var exist = context.Member.FirstOrDefault(x => x.Id == memberRequest.Id);
                if (exist != null)
                {
                    context.Member.Remove(exist);
                    await context.SaveChangesAsync();
                    return HttpStatusCode.OK;
                }
                return HttpStatusCode.BadRequest;
            }
            return HttpStatusCode.BadRequest;
        }
    }
}