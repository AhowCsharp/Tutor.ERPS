using AutoMapper;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Repository_Interfaces;
using TUTOR.Repository.EF;
using TUTOR.Repository.Repositories;
using ZXing.Aztec.Internal;

namespace TUTOR.Repository
{
    public class TokenRepository : EFRepositoryBase<ApiTokenManage, TokenDTO, int>, ITokenRepository
    {
        public TokenRepository(IMapper mapper, TUTORERPSContext context) : base(mapper, context)
        {
        }

        public TokenDTO Get(string token, string ip)
        {
            var data = _context.ApiTokenManage.SingleOrDefault(x => x.ApiToken == token);
            var pass = data != null ? true : false;

            if (!pass)
            {
                return null;
            }
            else
            {
                var dto = new TokenDTO
                {
                    Token = data.ApiToken,
                    IP = data.Ip,
                    StartDate = data.TokenStartTime,
                    EndDate = data.TokenEndTime
                };
                return dto;
            }
        }

        public void Log(TokenLogDTO dto)
        {
            var log = new SystemErrorLog
            {
                Token = dto.Token,
                Ip = dto.IP,
                Api = dto.RequestUrl,
                WebRequest = dto.StartDate.HasValue ? dto.StartDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                WebResponse = dto.EndDate.HasValue ? dto.EndDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                EntryDate = dto.LogDate
            };
            _context.SystemErrorLog.Add(log);
            _context.SaveChanges();
        }
    }
}