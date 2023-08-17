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
    public class SentenceManageRepository : EFRepositoryBase<SentenceManage, SentenceManageDTO, int>, ISentenceManageRepository
    {
        public SentenceManageRepository(IMapper mapper, TUTORERPSContext context) : base(mapper, context)
        {
        }

        public async Task<IEnumerable<SentenceManageDTO>> GetSentenceManageListAsync()
        {
            var data = await _context.SentenceManage.OrderBy(x => x.QuestionTypeId).ToListAsync();

            return data.Select(_mapper.Map<SentenceManageDTO>);
        }

        public async Task<IEnumerable<SentenceTypeDTO>> GetSentenceTypeListAsync()
        {
            var data = await _context.SentenceType.ToListAsync();

            return data.Select(_mapper.Map<SentenceTypeDTO>);
        }

        public async Task<bool> CreateSentenceAsync(SentenceManageDTO sentenceManageDTO)
        {
            var data = _mapper.Map<SentenceManage>(sentenceManageDTO);
            try
            {
                var exsit = _context.SentenceManage.SingleOrDefaultAsync(x => x.Mp3FileName == data.Mp3FileName);
                if (exsit != null)
                {
                    return false;
                }
                await _context.SentenceManage.AddAsync(data);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}