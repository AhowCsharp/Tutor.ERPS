using System.Net;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Models.Requests;
using TUTOR.Biz.Models;
using TUTOR.Biz.Repository_Interfaces.Base;
using X.PagedList;

namespace TUTOR.Biz.Repository_Interfaces
{
    public interface IErrorWordLogRepository : IRepository<ErrorWordLogDTO, int>
    {
        Task<IEnumerable<ErrorWordLogDTO>> GetErrorWordLogsAsync(int? level, string gamer);

        Task<bool> CreateWordsLogAsync(ErrorWordLogDTO gamerWordsLogDTO);
    }
}