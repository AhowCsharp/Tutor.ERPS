using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Repository_Interfaces.Base;

namespace TUTOR.Biz.Repository_Interfaces
{
    public interface ITokenRepository : IRepository<TokenDTO, int>
    {
        TokenDTO Get(string token, string ip);

        void Log(TokenLogDTO dto);
    }
}