using Swashbuckle.AspNetCore.Annotations;
using TUTOR.Biz.Domain.DTO;

namespace TUTOR.Biz.Models.Responses.Member
{
    public class MemberResponse
    {
        public MemberResponse(IEnumerable<MemberDTO> list)
        {
            List = list;
        }

        public IEnumerable<MemberDTO> List { get; set; }
    }

}