﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace TUTOR.Repository.EF
{
    public partial class SentenceManage
    {
        public int Id { get; set; }
        public int? QuestionTypeId { get; set; }
        public string QuestionSentence { get; set; }
        public string Mp3FileName { get; set; }
        public string Mp3FileUrl { get; set; }
        public string QuestionAnswer { get; set; }
        public int? StudyLevel { get; set; }
        public string QuestionSentenceChinese { get; set; }
    }
}