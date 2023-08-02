using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUTOR.Biz.Models.Requests
{
    public class MemberRequest
    {
        public string? Account { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }

        public int? Status { get; set; }

        public int? StudyLevel { get; set; }

        public int? BeDeleted { get; set; }
        public int? Id { get; set; }
        public string? Creator { get; set; }

        public string? Editor { get; set; }
        public string? Name { get; set; }

        /// <summary>
        /// 開始日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 結束日期
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}