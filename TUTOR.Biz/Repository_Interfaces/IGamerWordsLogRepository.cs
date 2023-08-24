using System.Net;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Models.Requests;
using TUTOR.Biz.Models;
using TUTOR.Biz.Repository_Interfaces.Base;
using X.PagedList;

namespace TUTOR.Biz.Repository_Interfaces
{
    public interface IGamerWordsLogRepository : IRepository<GamerWordsLogDTO, int>
    {
        Task<IEnumerable<GamerWordsLogDTO>> GetGamerWordsLogsAsync(int? level, string gamer);

        Task<bool> CreateWordsLogAsync(GamerWordsLogDTO gamerWordsLogDTO);
    }
}