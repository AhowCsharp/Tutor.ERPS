﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace TUTOR.Repository.EF
{
    public partial class WebToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Ip { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string EntryUser { get; set; }
        public DateTime? EntryDate { get; set; }
        public byte[] ModifyUser { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}