using System.Net;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Models.Requests;
using TUTOR.Biz.Models;
using TUTOR.Biz.Repository_Interfaces.Base;
using X.PagedList;

namespace TUTOR.Biz.Repository_Interfaces
{
    public interface ISentenceManageRepository : IRepository<SentenceManageDTO, int>
    {
        Task<IEnumerable<SentenceManageDTO>> GetSentenceManageListAsync();

        Task<IEnumerable<SentenceTypeDTO>> GetSentenceTypeListAsync();

        Task<bool> CreateSentenceAsync(SentenceManageDTO sentenceManageDTO);
    }
}