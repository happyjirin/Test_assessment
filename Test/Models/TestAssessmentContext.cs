using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Test.Models
{
    public partial class TestAssessmentContext : DbContext
    {
        public TestAssessmentContext()
        {
        }

        public TestAssessmentContext(DbContextOptions<TestAssessmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Platform> Platforms { get; set; }
        public virtual DbSet<Well> Wells { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=TestAssessment;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Platform>(entity =>
            {
                entity.ToTable("Platform");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.UniqueName).HasMaxLength(250);
            });

            modelBuilder.Entity<Well>(entity =>
            {
                entity.ToTable("Well");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.UniqueName).HasMaxLength(250);

                entity.HasOne(d => d.Platform)
                    .WithMany(p => p.well)
                    .HasForeignKey(d => d.PlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Well_Well");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
