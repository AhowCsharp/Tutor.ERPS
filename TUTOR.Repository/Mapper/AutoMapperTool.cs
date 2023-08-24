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
            CreateMap<SentenceTypeDTO, SentenceTypeRequest>().ReverseMap();
            CreateMap<SentenceManageDTO, SentenceManage>().ReverseMap();
            CreateMap<SentenceDTO, SentenceManage>().ReverseMap();
            CreateMap<GameWordsDTO, GameWords>().ReverseMap();
            CreateMap<GamerWordsLogDTO, GamerWordsLog>().ReverseMap();
            CreateMap<ErrorWordLogDTO, ErrorWordLog>().ReverseMap();
            CreateMap<SentenceTypeDTO, SentenceType>().ReverseMap();
            CreateMap<TokenLogDTO, SystemErrorLog>().ReverseMap();
            CreateMap<StudentAnswerLog, StudentAnswerLogDTO>().ReverseMap();
            CreateMap<AdminManage, AdminManageDTO>().ReverseMap();
            CreateMap<MemberDTO, Member>().ReverseMap();
            CreateMap<MemberRequest, MemberDTO>()
            .ForMember(dest => dest.id, opt =>
                opt.Condition(src => src.id != 0))  // 指定條件
            .ReverseMap();

            //CreateMap<SentenceManageDTO, SentenceManageRequest>().ReverseMap();
            //CreateMap<AdminOAInviteToken, AdminOAInviteTokenDTO>().ReverseMap();
            //CreateMap<Application, ApplicationDTO>().ReverseMap();
        }
    }
}