using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUTOR.Biz.Models.Requests
{
    public class SentenceTypeRequest
    {
        public string type { get; set; }
        public int studyLevel { get; set; }
    }
}