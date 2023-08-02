using TUTOR.Biz.Domain.DTO;

namespace TUTOR.Biz.Repository_Interfaces
{
    public interface ITokenRepository
    {
        TokenDTO Get(string token,string ip);

        void Log(TokenLogDTO dto);
    }
}
