using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUTOR.Biz.Models.Requests
{
    public class SentenceManageEditRequest
    {
        public IFormFile? mp3File { get; set; }
        public string questionSentence { get; set; }
        public string questionSentenceChinese { get; set; }
        public string typeName { get; set; }
        public string questionAnswer { get; set; }

        public int id { get; set; }
    }
}