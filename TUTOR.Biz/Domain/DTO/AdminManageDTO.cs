using TUTOR.Biz.SeedWork;

namespace TUTOR.Biz.Domain.DTO
{
    public class AdminManageDTO : IDTO
    {
        public int id { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public int adminLevel { get; set; }
        public DateTime createDate { get; set; }
        public int status { get; set; }
        public string jwttoken { get; set; }
    }
}