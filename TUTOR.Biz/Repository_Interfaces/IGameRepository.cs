using System.Net;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Models.Requests;
using TUTOR.Biz.Models;
using TUTOR.Biz.Repository_Interfaces.Base;
using X.PagedList;

namespace TUTOR.Biz.Repository_Interfaces
{
    public interface IGameRepository : IRepository<GameWordsDTO, int>
    {
        Task<IEnumerable<GameWordsDTO>> GetGameListAsync(int? level, List<string> avoidWords, List<string> againWords);

        Task<IEnumerable<GameWordsDTO>> GetGameListAsync();

        Task<bool> UpdateGameMp3UrlAsync(GameWordsDTO gameWordsDTO);

        Task<bool> UpdateGameWordsAsync(IEnumerable<GameWordsDTO> gameWordsDTOs);

        Task<GameWordsDTO> GetRepeatWordAsync(string word);
    }
}