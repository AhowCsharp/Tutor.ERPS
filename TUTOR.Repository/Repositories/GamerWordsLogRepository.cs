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
using TUTOR.Biz.Helpers;

namespace TUTOR.Repository.Repositories
{
    public class GamerWordsLogRepository : EFRepositoryBase<GamerWordsLog, GamerWordsLogDTO, int>, IGamerWordsLogRepository
    {
        public GamerWordsLogRepository(IMapper mapper, TUTORERPSContext context) : base(mapper, context)
        {
        }

        public async Task<bool> GetGameListAsync(GamerWordsLogDTO gamerWordsLogDTO)
        {
            try
            {
                var data = _mapper.Map<GamerWordsLog>(gamerWordsLogDTO);
                await _context.GamerWordsLog.AddAsync(data);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<GamerWordsLogDTO>> GetGamerWordsLogsAsync(int level, string gamer)
        {
            var today = DateTimeHelper.TaipeiNow;
            var twentyFourHoursAgo = today.AddHours(-24);
            var noNeed = await _context.GamerWordsLog.Where(x => x.CreateDate <= twentyFourHoursAgo && x.Gamer == gamer).ToListAsync();
            if (noNeed.Count() > 0)
            {
                _context.GamerWordsLog.RemoveRange(noNeed);
                await _context.SaveChangesAsync();
            }
            var data = await _context.GamerWordsLog
                .Where(x => x.Gamer == gamer && x.HardLevel == level && x.CreateDate >= twentyFourHoursAgo && x.CreateDate <= today)
                .ToListAsync();

            return data.Select(_mapper.Map<GamerWordsLogDTO>);
        }

        public async Task<bool> CreateWordsLogAsync(GamerWordsLogDTO gamerWordsLogDTO)
        {
            var data = _mapper.Map<GamerWordsLog>(gamerWordsLogDTO);
            await _context.GamerWordsLog.AddAsync(data);
            await _context.SaveChangesAsync();  // 將數據寫入數據庫
            return true;
        }
    }
}