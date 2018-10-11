using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using storagecore.Abstractions.Context;
using Survey.DomainModelLayer.Entities;

namespace Survey.InfrastructureLayer.Context
{
    public class ApplicationDbContext : DbContext, IEntityContext
    {
        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<InputTypes> InputTypes { get; set; }
        public virtual DbSet<OptionChoises> OptionChoises { get; set; }
        public virtual DbSet<OptionGroups> OptionGroups { get; set; }
        public virtual DbSet<Pages> Pages { get; set; }
        public virtual DbSet<QuestionOptions> QuestionOptions { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<SurveySections> SurveySections { get; set; }
        public virtual DbSet<SurveySectoinRespondents> SurveySectoinRespondents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {
        }

        public ApplicationDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(SqlConnectionFactory.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answers>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.HasOne(d => d.QuestionOption)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionOptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answers_Question_Options");

                entity.HasOne(d => d.Respondent)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.RespondentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answers_Respondents");
            });

            modelBuilder.Entity<InputTypes>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            });

            modelBuilder.Entity<OptionChoises>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.HasOne(d => d.OptionGroup)
                    .WithMany(p => p.OptionChoises)
                    .HasForeignKey(d => d.OptionGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OptionChoises_OptionGroups");
            });

            modelBuilder.Entity<OptionGroups>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            });

            modelBuilder.Entity<Pages>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pages_SurveySections");
            });

            modelBuilder.Entity<QuestionOptions>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.HasOne(d => d.OptionChoise)
                    .WithMany(p => p.QuestionOptions)
                    .HasForeignKey(d => d.OptionChoiseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_Options_OptionChoises");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionOptions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_Options_Questions");
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.HasOne(d => d.InputTypes)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.InputTypesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questions_InputTypes");

                entity.HasOne(d => d.OptionGroup)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.OptionGroupId)
                    .HasConstraintName("FK_Questions_OptionGroups");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questions_Pages");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Questions_Questions");
            });

            modelBuilder.Entity<SurveySections>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SurveySections)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveySections_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            });

            modelBuilder.Entity<Respondents>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Respondents)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Respondents_Users");
            });

            modelBuilder.Entity<SurveySectoinRespondents>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.HasOne(d => d.Respondent)
                    .WithMany(p => p.SurveySectoinRespondents)
                    .HasForeignKey(d => d.RespondentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveySectoin_Respondents_Respondents");

                entity.HasOne(d => d.SurveySection)
                    .WithMany(p => p.SurveySectoinRespondents)
                    .HasForeignKey(d => d.SurveySectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveySectoin_Respondents_SurveySection");
            });
        }
    }


}
