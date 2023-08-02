using AutoMapper;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Repository.EF;

namespace TUTOR.Repository.Mapper
{
    public class AutoMapperTool : Profile
    {
        public AutoMapperTool()
        {
            //CreateMap<AdminOA, AdminOADTO>().ReverseMap();
            //CreateMap<AdminOAInviteToken, AdminOAInviteTokenDTO>().ReverseMap();
            //CreateMap<Application, ApplicationDTO>().ReverseMap();
        }
    }
}