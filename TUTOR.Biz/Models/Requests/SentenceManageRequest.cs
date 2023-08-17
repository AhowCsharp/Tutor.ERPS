using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUTOR.Biz.Models.Requests
{
    public class SentenceManageRequest
    {
        public int Id { get; set; }
        public int? QuestionTypeId { get; set; }
        public string QuestionSentence { get; set; }
        public string Mp3FileName { get; set; }
        public string Mp3FileUrl { get; set; }
        public string QuestionAnswer { get; set; }
        public int? StudyLevel { get; set; }
        public string QuestionSentenceChinese { get; set; }

        public IFormFile Mp3File { get; set; }
        public string? TypeName { get; set; }
    }
}