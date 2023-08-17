using Swashbuckle.AspNetCore.Annotations;
using TUTOR.Biz.Domain.DTO;

namespace TUTOR.Biz.Models.Responses.SentenceManage
{
    public class SentenceManageResponse
    {
        public SentenceManageResponse(IEnumerable<SentenceManageDTO> list)
        {
            List = list;
        }

        public IEnumerable<SentenceManageDTO> List { get; set; }
    }
}