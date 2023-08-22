using Swashbuckle.AspNetCore.Annotations;
using TUTOR.Biz.Domain.DTO;

namespace TUTOR.Biz.Models.Responses.AdminManage
{
    public class AdminManageResponse
    {
        public AdminManageResponse(AdminManageDTO? adminManage, MemberDTO? member)
        {
            adminManageDTO = adminManage;
            memberDTO = member;
        }

        public AdminManageDTO? adminManageDTO { get; set; }

        public MemberDTO? memberDTO { get; set; }
    }

}