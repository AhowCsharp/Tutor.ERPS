using Swashbuckle.AspNetCore.Annotations;
using TUTOR.Biz.Domain.DTO;

namespace TUTOR.Biz.Models.Responses.SentenceManage
{
    public class SentenceResponse
    {
        public SentenceResponse(IEnumerable<SentenceDTO>? list)
        {
            List = list;
        }

        public IEnumerable<SentenceDTO>? List { get; set; }
    }
}