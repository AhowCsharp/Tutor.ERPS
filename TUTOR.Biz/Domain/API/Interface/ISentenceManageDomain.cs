using Microsoft.AspNetCore.Http;
using TUTOR.Biz.Models;
using TUTOR.Biz.Models.Requests;
using TUTOR.Biz.Models.Responses.SentenceManage;
using TUTOR.Biz.Models.Responses.SentenceType;

namespace TUTOR.Biz.Domain.API.Interface
{
    public interface ISentenceManageDomain
    {
        Task<SentenceManageResponse> GetSentenceManageListAsync();

        Task<bool> CreateSentenceManageAsync(SentenceManageRequest sentenceManageRequest);

        Task<SentenceTypeResponse> GetSentenceTypeListAsync();
    }
}