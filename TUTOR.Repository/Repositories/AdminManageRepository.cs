using AutoMapper;
using TUTOR.Repository.Repositories;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Repository_Interfaces;
using TUTOR.Repository.EF;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Collections;
using TUTOR.Biz.Models.Requests;

namespace TUTOR.Repository.Repositories
{
    public class AdminManageRepository : EFRepositoryBase<AdminManage, AdminManageDTO, int>, IAdminManageRepository
    {
        public AdminManageRepository(IMapper mapper, TUTORERPSContext context) : base(mapper, context)
        {
        }

        public async Task<AdminManageDTO?> GetAdminManageAsync(LoginRequest loginRequest)
        {
            var data = await _context.AdminManage.SingleOrDefaultAsync(x => x.Account == loginRequest.account && x.Password == loginRequest.password);

            return _mapper.Map<AdminManageDTO>(data);
        }

        public async Task<MemberDTO?> GetMemberAsync(LoginRequest loginRequest)
        {
            var data = await _context.Member.SingleOrDefaultAsync(x => x.Account == loginRequest.account && x.Password == loginRequest.password);

            return _mapper.Map<MemberDTO>(data);
        }
    }
}