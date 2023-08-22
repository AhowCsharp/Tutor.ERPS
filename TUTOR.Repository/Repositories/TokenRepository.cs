using AutoMapper;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Models.Requests;
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

        public void Log(TokenLogDTO TokenLogDTO)
        {
            var data = _mapper.Map<SystemErrorLog>(TokenLogDTO);
            _context.SystemErrorLog.Add(data);
            _context.SaveChanges();
        }
    }
}