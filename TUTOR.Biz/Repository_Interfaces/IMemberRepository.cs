using System.Net;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Models.Requests;
using TUTOR.Biz.Models;

namespace TUTOR.Biz.Repository_Interfaces
{
    public interface IMemberRepository
    {
        IEnumerable<MemberDTO>? GetList(int page);

        MemberDTO? Get(MemberRequest memberRequest);

        Task<HttpStatusCode> EditOrCreate(MemberRequest memberRequest);

        Task<HttpStatusCode> Delete(MemberRequest memberRequest);

        Task<StatusResponse> InsertExcelDatas(IEnumerable<MemberDTO> memberDTOs);
    }
}