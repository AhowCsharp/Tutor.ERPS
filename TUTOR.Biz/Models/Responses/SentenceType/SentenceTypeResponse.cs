using Swashbuckle.AspNetCore.Annotations;
using TUTOR.Biz.Domain.DTO;

namespace TUTOR.Biz.Models.Responses.SentenceType
{
    public class SentenceTypeResponse
    {
        public SentenceTypeResponse(IEnumerable<SentenceTypeDTO> list)
        {
            List = list;
        }

        public IEnumerable<SentenceTypeDTO> List { get; set; }
    }
}