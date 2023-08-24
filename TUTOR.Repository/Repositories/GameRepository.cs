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
using NPOI.POIFS.Crypt.Dsig;

namespace TUTOR.Repository.Repositories
{
    public class GameRepository : EFRepositoryBase<GameWords, GameWordsDTO, int>, IGameRepository
    {
        public GameRepository(IMapper mapper, TUTORERPSContext context) : base(mapper, context)
        {
        }

        public async Task<IEnumerable<GameWordsDTO>> GetGameListAsync(int level, List<string> avoidWords, List<string> againWords)
        {
            var data = await _context.GameWords
            .Where(x => x.HardLevel == level && !avoidWords.Contains(x.Word) && againWords.Contains(x.Word))
            .ToListAsync();

            return data.Select(_mapper.Map<GameWordsDTO>);
        }

        public async Task<IEnumerable<GameWordsDTO>> GetGameListAsync()
        {
            var data = await _context.GameWords
            .ToListAsync();

            return data.Select(_mapper.Map<GameWordsDTO>);
        }

        public async Task<GameWordsDTO> GetRepeatWordAsync(string word)
        {
            var data = await _context.GameWords.FirstOrDefaultAsync(x => x.Word == word);

            return _mapper.Map<GameWordsDTO>(data);
        }

        public async Task<bool> UpdateGameMp3UrlAsync(GameWordsDTO gameWordsDTO)
        {
            var exist = _context.GameWords.SingleOrDefault(x => x.Id == gameWordsDTO.Id);
            if (exist != null)
            {
                _mapper.Map(gameWordsDTO, exist);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateGameWordsAsync(IEnumerable<GameWordsDTO> gameWordsDTOs)
        {
            var entitys = gameWordsDTOs.Select(_mapper.Map<GameWords>);
            await _context.AddRangeAsync(entitys);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}