using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUTOR.Biz.Models
{
    public class DefaultPager
    {
        public int TotalCount { get; set; }

        public int DisplayCount { get; set; }

        public int PageIndex { get; set; }

        public DefaultPager(int displayCount, int pageIndex, int totalCount)
        {
            this.DisplayCount = displayCount;
            this.PageIndex = pageIndex;
            this.TotalCount = totalCount;
        }
    }
}