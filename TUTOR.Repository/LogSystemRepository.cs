using AutoMapper;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Repository_Interfaces;
using TUTOR.Repository.EF;
using System.Threading.Tasks;

namespace TUTOR.Repository
{
    public class LogSystemRepository : ILogSystemRepository
    {
        private readonly IMapper mapper;
        private readonly TUTORERPSContext context;

        public LogSystemRepository(IMapper mapper, TUTORERPSContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public void Log(LogSystemDTO dto)
        {
            var data = mapper.Map<LogSystem>(dto);

            context.LogSystem.Add(data);
            context.SaveChanges();
        }

        public async Task LogAsync(LogSystemDTO dto)
        {
            var data = mapper.Map<LogSystem>(dto);
            await context.LogSystem.AddAsync(data);
            await context.SaveChangesAsync();
        }
    }
}