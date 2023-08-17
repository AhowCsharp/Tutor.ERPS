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

    public class Member
    {
        [SwaggerSchema("帳號")]
        public string Account { get; set; }

        [SwaggerSchema("信箱")]
        public string Email { get; set; }

        [SwaggerSchema("狀態")]
        public int Status { get; set; }

        [SwaggerSchema("學習等級")]
        public int? StudyLevel { get; set; }

        [SwaggerSchema("是否刪除")]
        public int BeDeleted { get; set; }

        [SwaggerSchema("姓名")]
        public string? Name { get; set; }

        [SwaggerSchema("開始日期")]
        public DateTime? StartDate { get; set; }

        [SwaggerSchema("結束日期")]
        public DateTime? EndDate { get; set; }
    }
}