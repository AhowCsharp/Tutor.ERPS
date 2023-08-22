using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUTOR.Biz.Models.Requests
{
    public class StudentAnswerRequest
    {
        public string account { get; set; }

        public string name { get; set; }
        public string level { get; set; }

        public string answer { get; set; }

        public int timeCost { get; set; }


        public string questionType { get; set; }

        public int questionNum { get; set; }

    }

}