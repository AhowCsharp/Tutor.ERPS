using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUTOR.Biz.Models.Requests
{
    public class GameWordRequest 
    {
        public int id { get; set; }
        public string? word { get; set; }
        public int hardLevel { get; set; }
        public int? status { get; set; }
        public string? wordKind { get; set; }

        public string? mp3Url { get; set; }

        public string? wordChinese { get; set; }
    }
}