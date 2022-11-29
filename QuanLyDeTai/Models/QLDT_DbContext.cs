using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuanLyDeTai.Models
{
    public partial class QLDT_DbContext : DbContext
    {
        public QLDT_DbContext()
        {
        }

        public QLDT_DbContext(DbContextOptions<QLDT_DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assess> Assesses { get; set; } = null!;
        public virtual DbSet<Assessment> Assessments { get; set; } = null!;
        public virtual DbSet<Faculty> Faculties { get; set; } = null!;
        public virtual DbSet<Lecturer> Lecturers { get; set; } = null!;
        public virtual DbSet<Register> Registers { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Topic> Topics { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=QLDT_Db;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assess>(entity =>
            {
                entity.HasKey(e => new { e.AssessmentId, e.LecturerId, e.TopicId })
                    .HasName("PK__ASSESS__848E5DA0B4395153");

                entity.ToTable("ASSESS");

                entity.Property(e => e.AssessmentId).HasColumnName("AssessmentID");

                entity.Property(e => e.LecturerId).HasColumnName("LecturerID");

                entity.Property(e => e.TopicId).HasColumnName("TopicID");

                entity.Property(e => e.PositionAssess)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.Assesses)
                    .HasForeignKey(d => d.AssessmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ASSESS_ASSESSMENT");

                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.Assesses)
                    .HasForeignKey(d => d.LecturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ASSESS_LECTURER");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Assesses)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ASSESS_TOPIC");
            });

            modelBuilder.Entity<Assessment>(entity =>
            {
                entity.ToTable("ASSESSMENT");

                entity.Property(e => e.AssessmentId).HasColumnName("AssessmentID");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("FACULTY");

                entity.Property(e => e.FacultyId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FacultyID");

                entity.Property(e => e.FacultyName)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Lecturer>(entity =>
            {
                entity.ToTable("LECTURER");

                entity.Property(e => e.LecturerId).HasColumnName("LecturerID");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Contract)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.FacultyId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FacultyID");

                entity.Property(e => e.LecturerCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LecturerName).HasMaxLength(255);

                entity.Property(e => e.LevelCurrent)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Major)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Lecturers)
                    .HasForeignKey(d => d.FacultyId)
                    .HasConstraintName("FK_LECTURER_FACULTY");
            });

            modelBuilder.Entity<Register>(entity =>
            {
                entity.HasKey(e => new { e.LecturerId, e.TopicId })
                    .HasName("PK__REGISTER__9A5A59EA04BC708A");

                entity.ToTable("REGISTER");

                entity.Property(e => e.LecturerId).HasColumnName("LecturerID");

                entity.Property(e => e.TopicId).HasColumnName("TopicID");

                entity.Property(e => e.LevelLecturer)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PositionTopic)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.Registers)
                    .HasForeignKey(d => d.LecturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_REGISTER_LECTURER");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Registers)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_REGISTER_TOPIC");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("STATUS");

                entity.Property(e => e.StatusId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StatusID");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("TOPIC");

                entity.Property(e => e.TopicId).HasColumnName("TopicID");

                entity.Property(e => e.AssessmentDate).HasColumnType("smalldatetime");

                entity.Property(e => e.AssessmentId).HasColumnName("AssessmentID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LevelTopic)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Rating)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Reason)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ResearchField)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StatusID");

                entity.Property(e => e.TopicName).HasMaxLength(255);

                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.Topics)
                    .HasForeignKey(d => d.AssessmentId)
                    .HasConstraintName("FK_TOPIC_ASSESSMENT");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Topics)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_TOPIC_STATUS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
