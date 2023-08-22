﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TUTOR.Repository.EF
{
    public partial class TUTORERPSContext : DbContext
    {
        public TUTORERPSContext()
        {
        }

        public TUTORERPSContext(DbContextOptions<TUTORERPSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminManage> AdminManage { get; set; }
        public virtual DbSet<AllowIpManage> AllowIpManage { get; set; }
        public virtual DbSet<ApiTokenManage> ApiTokenManage { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<SentenceManage> SentenceManage { get; set; }
        public virtual DbSet<SentenceType> SentenceType { get; set; }
        public virtual DbSet<StudentAnswerLog> StudentAnswerLog { get; set; }
        public virtual DbSet<SystemErrorLog> SystemErrorLog { get; set; }
        public virtual DbSet<WebToken> WebToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminManage>(entity =>
            {
                entity.Property(e => e.Account).IsRequired();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Jwttoken).HasColumnName("JWTtoken");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<AllowIpManage>(entity =>
            {
                entity.Property(e => e.AllowIp).HasColumnName("Allow_Ip");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ApiTokenManage>(entity =>
            {
                entity.Property(e => e.ApiToken).HasColumnName("Api_Token");

                entity.Property(e => e.Ip).HasMaxLength(50);

                entity.Property(e => e.TokenActive).HasColumnName("Token_Active");

                entity.Property(e => e.TokenEndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Token_EndTime");

                entity.Property(e => e.TokenStartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Token_StartTime");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.Account).IsRequired();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Creator).HasMaxLength(50);

                entity.Property(e => e.Editor).HasMaxLength(50);

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SentenceManage>(entity =>
            {
                entity.Property(e => e.Mp3FileName)
                    .HasMaxLength(50)
                    .HasColumnName("Mp3_FileName");

                entity.Property(e => e.Mp3FileUrl).HasColumnName("Mp3_FileUrl");

                entity.Property(e => e.QuestionAnswer)
                    .HasMaxLength(50)
                    .HasColumnName("Question_Answer");

                entity.Property(e => e.QuestionSentence).HasColumnName("Question_Sentence");

                entity.Property(e => e.QuestionSentenceChinese).HasColumnName("Question_Sentence_Chinese");

                entity.Property(e => e.QuestionTypeId).HasColumnName("Question_Type_Id");

                entity.Property(e => e.StudyLevel).HasColumnName("Study_Level");
            });

            modelBuilder.Entity<SentenceType>(entity =>
            {
                entity.Property(e => e.StudyLevel).HasColumnName("Study_Level");

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<StudentAnswerLog>(entity =>
            {
                entity.Property(e => e.AnswerDate).HasColumnType("datetime");

                entity.Property(e => e.CostTime).HasMaxLength(50);

                entity.Property(e => e.QuestionNumber)
                    .HasMaxLength(50)
                    .HasColumnName("Question_Number");

                entity.Property(e => e.QuestionType)
                    .HasMaxLength(50)
                    .HasColumnName("Question_Type");

                entity.Property(e => e.StudentId).HasColumnName("Student_Id");
            });

            modelBuilder.Entity<SystemErrorLog>(entity =>
            {
                entity.Property(e => e.Api).HasMaxLength(50);

                entity.Property(e => e.EntryDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Entry_Date");

                entity.Property(e => e.EntryUser)
                    .HasMaxLength(50)
                    .HasColumnName("Entry_User");

                entity.Property(e => e.ErrorLevel)
                    .HasMaxLength(50)
                    .HasColumnName("Error_Level");

                entity.Property(e => e.Ip).HasMaxLength(50);

                entity.Property(e => e.WebMethod)
                    .HasMaxLength(50)
                    .HasColumnName("Web_Method");

                entity.Property(e => e.WebRequest).HasColumnName("Web_Request");

                entity.Property(e => e.WebResponse).HasColumnName("Web_Response");
            });

            modelBuilder.Entity<WebToken>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.EntryUser).HasMaxLength(50);

                entity.Property(e => e.Ip).HasMaxLength(50);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyUser).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}