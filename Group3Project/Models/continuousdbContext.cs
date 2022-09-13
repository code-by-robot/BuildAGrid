using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Group3Project.Models
{
    public partial class continuousdbContext : DbContext
    {
        public continuousdbContext()
        {
        }

        public continuousdbContext(DbContextOptions<continuousdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Builtplant> Builtplants { get; set; } = null!;
        public virtual DbSet<Plantprop> Plantprops { get; set; } = null!;
        public virtual DbSet<Usertable> Usertables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql($"server={Secret.host};user={Secret.userId};password={Secret.password};database={Secret.address}", ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            modelBuilder.Entity<Builtplant>(entity =>
            {
                entity.ToTable("builtplant");

                entity.HasIndex(e => e.FuelId, "FuelId_idx");

                entity.Property(e => e.Ac).HasColumnName("AC");

                entity.Property(e => e.Npc).HasColumnName("NPC");

                entity.HasOne(d => d.Fuel)
                    .WithMany(p => p.Builtplants)
                    .HasForeignKey(d => d.FuelId)
                    .HasConstraintName("FuelId");
            });

            modelBuilder.Entity<Plantprop>(entity =>
            {
                entity.ToTable("plantprop");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AltCode).HasMaxLength(45);

                entity.Property(e => e.FuelType).HasMaxLength(45);

                entity.Property(e => e.FuelTypeCode).HasMaxLength(45);

                entity.Property(e => e.RampRate).HasColumnType("bit(1)");
            });

            modelBuilder.Entity<Usertable>(entity =>
            {
                entity.ToTable("usertable");

                entity.HasIndex(e => e.BpId, "BpId_idx");

                entity.Property(e => e.UserId).HasMaxLength(45);

                entity.HasOne(d => d.Bp)
                    .WithMany(p => p.Usertables)
                    .HasForeignKey(d => d.BpId)
                    .HasConstraintName("BpId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
