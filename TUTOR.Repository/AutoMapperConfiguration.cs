using AutoMapper;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Repository.EF;

namespace LineTag.Infrastructure
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            //CreateMap<AdminOA, AdminOADTO>().ReverseMap();
        }
    }
}