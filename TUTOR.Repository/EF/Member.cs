﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace TUTOR.Repository.EF
{
    public partial class Member
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? StudyLevel { get; set; }
        public int Status { get; set; }
        public int BeDeleted { get; set; }
        public string Password { get; set; }
        public string Creator { get; set; }
        public string Editor { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Name { get; set; }
    }
}