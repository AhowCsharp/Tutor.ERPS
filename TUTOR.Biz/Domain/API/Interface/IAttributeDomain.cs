namespace TUTOR.Biz.Domain.API.Interface
{
    public interface IAttributeDomain
    {
        bool VerifyToken(string token, string ip);

        void TokenLog(string token);
    }
}
