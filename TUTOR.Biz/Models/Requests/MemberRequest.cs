using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUTOR.Biz.Models.Requests
{
    public class MemberRequest
    {
        public string account { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public int status { get; set; }

        public int studyLevel { get; set; }

        public int beDeleted { get; set; }

        public string creator { get; set; }

        public string editor { get; set; }
        public string name { get; set; }
        public int id { get; set; }

        /// <summary>
        /// 開始日期
        /// </summary>
        public DateTime startDate { get; set; }

        /// <summary>
        /// 結束日期
        /// </summary>
        public DateTime endDate { get; set; }

        public DateTime createDate { get; set; }
    }

    public class MemberEditRequest
    {
        public List<MemberRequest> requests { get; set; }
    }
}