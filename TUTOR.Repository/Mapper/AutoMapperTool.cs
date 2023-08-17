using AutoMapper;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Repository.EF;
using TUTOR.Biz.Models.Requests;
using TUTOR.Biz.Models.Responses.SentenceManage;

namespace TUTOR.Repository.Mapper
{
    public class AutoMapperTool : Profile
    {
        public AutoMapperTool()
        {
            CreateMap<SentenceManageDTO, SentenceManageRequest>().ReverseMap();
            CreateMap<SentenceManageDTO, SentenceManage>().ReverseMap();
            CreateMap<SentenceTypeDTO, SentenceType>().ReverseMap();
            CreateMap<MemberDTO, Member>().ReverseMap();
            //CreateMap<SentenceManageDTO, SentenceManageRequest>().ReverseMap();
            //CreateMap<AdminOAInviteToken, AdminOAInviteTokenDTO>().ReverseMap();
            //CreateMap<Application, ApplicationDTO>().ReverseMap();
        }
    }
}