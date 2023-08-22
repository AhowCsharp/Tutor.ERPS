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

namespace TUTOR.Repository.Repositories
{
    public class MemberRepository : EFRepositoryBase<Member, MemberDTO, int>, IMemberRepository
    {
        public MemberRepository(IMapper mapper, TUTORERPSContext context) : base(mapper, context)
        {
        }

        public async Task<IEnumerable<MemberDTO>> GetMemberListAsync()
        {
            var data = await _context.Member.Where(x => x.BeDeleted == 0).ToListAsync();

            return _mapper.Map<IEnumerable<MemberDTO>>(data);
        }

        public async Task<MemberDTO> GetMemberAsync(string account,string name)
        {
            var data = await _context.Member.FirstOrDefaultAsync(x => x.BeDeleted == 0 && x.Account == account && x.Name == name);

            return _mapper.Map<MemberDTO>(data);
        }

        public async Task<bool> AddStudentsFromExcel(IEnumerable<MemberDTO> memberDTOs)
        {
            try
            {
                var members = _mapper.Map<IEnumerable<Member>>(memberDTOs);
                await _context.Member.AddRangeAsync(members);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> EditMember(MemberDTO dto)
        {
            var exist = await _context.Member.FirstOrDefaultAsync(x => x.Id == dto.id);
            if (exist == null)
            {
                return false;
            }
            else
            {
                _mapper.Map(dto, exist);  // 

                await _context.SaveChangesAsync();

                return true;

            }
        }
    }
}