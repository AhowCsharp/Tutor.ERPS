﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace TUTOR.Repository.EF
{
    public partial class SystemErrorLog
    {
        public int Id { get; set; }
        public string ErrorLevel { get; set; }
        public string Token { get; set; }
        public string Ip { get; set; }
        public string Api { get; set; }
        public string WebMethod { get; set; }
        public string WebRequest { get; set; }
        public string WebResponse { get; set; }
        public DateTime? EntryDate { get; set; }
        public string EntryUser { get; set; }
        public string Message { get; set; }
    }
}