using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Models;

namespace TUTOR.Biz.Services
{
    public class UserService
    {
        private CurrentUser _user = null;

        private CurrentUser User
        {
            get
            {
                return _user;
            }
        }

        public CurrentUser GetCurrentUser()
        {
            return User;
        }

        public void SetApToken(string apToken)
        {
            _user = _user ?? new CurrentUser();

            _user.ApToken = apToken;
        }
    }
}