using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Test
{
    public partial class DryingWood_DBContext : DbContext
    {
        public DryingWood_DBContext()
        {
        }

        public DryingWood_DBContext(DbContextOptions<DryingWood_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chamber> Chambers { get; set; }
        public virtual DbSet<ChamberWood> ChamberWoods { get; set; }
        public virtual DbSet<PlanDrying> PlanDryings { get; set; }
        public virtual DbSet<SoftDl> SoftDls { get; set; }
        public virtual DbSet<SoftDl22> SoftDl22s { get; set; }
        public virtual DbSet<SoftDl25> SoftDl25s { get; set; }
        public virtual DbSet<SoftDl32> SoftDl32s { get; set; }
        public virtual DbSet<SoftDl40> SoftDl40s { get; set; }
        public virtual DbSet<SoftDl50> SoftDl50s { get; set; }
        public virtual DbSet<SoftDm> SoftDms { get; set; }
        public virtual DbSet<SoftDm22> SoftDm22s { get; set; }
        public virtual DbSet<SoftDm25> SoftDm25s { get; set; }
        public virtual DbSet<SoftDm32> SoftDm32s { get; set; }
        public virtual DbSet<SoftDm40> SoftDm40s { get; set; }
        public virtual DbSet<SoftDm50> SoftDm50s { get; set; }
        public virtual DbSet<SoftDmr> SoftDmrs { get; set; }
        public virtual DbSet<SoftDmr22> SoftDmr22s { get; set; }
        public virtual DbSet<SoftDmr25> SoftDmr25s { get; set; }
        public virtual DbSet<SoftDmr32> SoftDmr32s { get; set; }
        public virtual DbSet<SoftDmr40> SoftDmr40s { get; set; }
        public virtual DbSet<SoftDmr50> SoftDmr50s { get; set; }
        public virtual DbSet<SoftDrl> SoftDrls { get; set; }
        public virtual DbSet<SoftDrl22> SoftDrl22s { get; set; }
        public virtual DbSet<SoftDrl25> SoftDrl25s { get; set; }
        public virtual DbSet<SoftDrl32> SoftDrl32s { get; set; }
        public virtual DbSet<SoftDrl40> SoftDrl40s { get; set; }
        public virtual DbSet<SoftDrl50> SoftDrl50s { get; set; }
        public virtual DbSet<ValueWood> ValueWoods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-I6LAO3J\\SQLEXPRESS;Database=DryingWood_DB;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=DryingWood_DB;User Id=admin;password=admin;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Chamber>(entity =>
            {
                entity.ToTable("Chamber");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChamberHoursLeft).HasColumnName("chamberHoursLeft");

                entity.Property(e => e.ChamberHoursSpend).HasColumnName("chamberHoursSpend");

                entity.Property(e => e.ChamberWoodId).HasColumnName("chamberWood_id");

                entity.Property(e => e.PlanDryingId).HasColumnName("planDrying_id");

                entity.HasOne(d => d.ChamberWood)
                    .WithMany(p => p.Chambers)
                    .HasForeignKey(d => d.ChamberWoodId)
                    .HasConstraintName("FK__Chamber__chamber__42E1EEFE");

                entity.HasOne(d => d.PlanDrying)
                    .WithMany(p => p.Chambers)
                    .HasForeignKey(d => d.PlanDryingId)
                    .HasConstraintName("FK__Chamber__planDry__43D61337");
            });

            modelBuilder.Entity<ChamberWood>(entity =>
            {
                entity.ToTable("ChamberWood");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Position).HasColumnName("position");

                entity.Property(e => e.TimeHours).HasColumnName("timeHours");

                entity.Property(e => e.TypeWood)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("typeWood");

                entity.Property(e => e.ValueId).HasColumnName("value_id");

                entity.HasOne(d => d.Value)
                    .WithMany(p => p.ChamberWoods)
                    .HasForeignKey(d => d.ValueId)
                    .HasConstraintName("FK__ChamberWo__value__3E1D39E1");
            });

            modelBuilder.Entity<PlanDrying>(entity =>
            {
                entity.ToTable("PlanDrying");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HoursLeftDrying).HasColumnName("hoursLeftDrying");

                entity.Property(e => e.HoursSpendDrying).HasColumnName("hoursSpendDrying");

                entity.Property(e => e.MonthDrying)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("monthDrying");

                entity.Property(e => e.Utility).HasColumnName("utility");

                entity.Property(e => e.ValueChamber).HasColumnName("valueChamber");
            });

            modelBuilder.Entity<SoftDl>(entity =>
            {
                entity.ToTable("SoftDL");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HumidityH).HasColumnName("humidityH");

                entity.Property(e => e.HumidityK).HasColumnName("humidityK");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.SoftDl22Id).HasColumnName("softDL22_id");

                entity.Property(e => e.SoftDl25Id).HasColumnName("softDL25_id");

                entity.Property(e => e.SoftDl32Id).HasColumnName("softDL32_id");

                entity.Property(e => e.SoftDl40Id).HasColumnName("softDL40_id");

                entity.Property(e => e.SoftDl50Id).HasColumnName("softDL50_id");

                entity.HasOne(d => d.SoftDl22)
                    .WithMany(p => p.SoftDls)
                    .HasForeignKey(d => d.SoftDl22Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SoftDL_SoftDL22");

                entity.HasOne(d => d.SoftDl25)
                    .WithMany(p => p.SoftDls)
                    .HasForeignKey(d => d.SoftDl25Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SoftDL_SoftDL25");

                entity.HasOne(d => d.SoftDl32)
                    .WithMany(p => p.SoftDls)
                    .HasForeignKey(d => d.SoftDl32Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SoftDL_SoftDL32");

                entity.HasOne(d => d.SoftDl40)
                    .WithMany(p => p.SoftDls)
                    .HasForeignKey(d => d.SoftDl40Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SoftDL_SoftDL40");

                entity.HasOne(d => d.SoftDl50)
                    .WithMany(p => p.SoftDls)
                    .HasForeignKey(d => d.SoftDl50Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SoftDL_SoftDL50");
            });

            modelBuilder.Entity<SoftDl22>(entity =>
            {
                entity.ToTable("SoftDL22");

                entity.Property(e => e.SoftDl22Id).HasColumnName("softDL22_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDl25>(entity =>
            {
                entity.ToTable("SoftDL25");

                entity.Property(e => e.SoftDl25Id).HasColumnName("softDL25_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDl32>(entity =>
            {
                entity.ToTable("SoftDL32");

                entity.Property(e => e.SoftDl32Id).HasColumnName("softDL32_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDl40>(entity =>
            {
                entity.ToTable("SoftDL40");

                entity.Property(e => e.SoftDl40Id).HasColumnName("softDL40_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDl50>(entity =>
            {
                entity.HasKey(e => e.SoftDl40Id);

                entity.ToTable("SoftDL50");

                entity.Property(e => e.SoftDl40Id).HasColumnName("softDL40_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDm>(entity =>
            {
                entity.ToTable("SoftDM");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HumidityH).HasColumnName("humidityH");

                entity.Property(e => e.HumidityK).HasColumnName("humidityK");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.SoftDm22Id).HasColumnName("softDM22_id");

                entity.Property(e => e.SoftDm25Id).HasColumnName("softDM25_id");

                entity.Property(e => e.SoftDm32Id).HasColumnName("softDM32_id");

                entity.Property(e => e.SoftDm40Id).HasColumnName("softDM40_id");

                entity.Property(e => e.SoftDm50Id).HasColumnName("softDM50_id");

                entity.HasOne(d => d.SoftDm22)
                    .WithMany(p => p.SoftDms)
                    .HasForeignKey(d => d.SoftDm22Id)
                    .HasConstraintName("FK_SoftDM_SoftDM221");

                entity.HasOne(d => d.SoftDm25)
                    .WithMany(p => p.SoftDms)
                    .HasForeignKey(d => d.SoftDm25Id)
                    .HasConstraintName("FK_SoftDM_SoftDM25");

                entity.HasOne(d => d.SoftDm32)
                    .WithMany(p => p.SoftDms)
                    .HasForeignKey(d => d.SoftDm32Id)
                    .HasConstraintName("FK_SoftDM_SoftDM321");

                entity.HasOne(d => d.SoftDm40)
                    .WithMany(p => p.SoftDms)
                    .HasForeignKey(d => d.SoftDm40Id)
                    .HasConstraintName("FK_SoftDM_SoftDM401");

                entity.HasOne(d => d.SoftDm50)
                    .WithMany(p => p.SoftDms)
                    .HasForeignKey(d => d.SoftDm50Id)
                    .HasConstraintName("FK_SoftDM_SoftDM501");
            });

            modelBuilder.Entity<SoftDm22>(entity =>
            {
                entity.ToTable("SoftDM22");

                entity.Property(e => e.SoftDm22Id).HasColumnName("softDm22_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDm25>(entity =>
            {
                entity.ToTable("SoftDM25");

                entity.Property(e => e.SoftDm25Id).HasColumnName("softDm25_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDm32>(entity =>
            {
                entity.HasKey(e => e.SoftDm30Id)
                    .HasName("PK_SoftDM32_1");

                entity.ToTable("SoftDM32");

                entity.Property(e => e.SoftDm30Id).HasColumnName("softDm30_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDm40>(entity =>
            {
                entity.ToTable("SoftDM40");

                entity.Property(e => e.SoftDm40Id).HasColumnName("softDm40_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDm50>(entity =>
            {
                entity.ToTable("SoftDM50");

                entity.Property(e => e.SoftDm50Id).HasColumnName("softDm50_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDmr>(entity =>
            {
                entity.ToTable("SoftDMR");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HumidityH).HasColumnName("humidityH");

                entity.Property(e => e.HumidityK).HasColumnName("humidityK");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.SoftDm22Id).HasColumnName("softDM22_id");

                entity.Property(e => e.SoftDm25Id).HasColumnName("softDM25_id");

                entity.Property(e => e.SoftDm32Id).HasColumnName("softDM32_id");

                entity.Property(e => e.SoftDm40Id).HasColumnName("softDM40_id");

                entity.Property(e => e.SoftDm50Id).HasColumnName("softDM50_id");

                entity.HasOne(d => d.SoftDm22)
                    .WithMany(p => p.SoftDmrs)
                    .HasForeignKey(d => d.SoftDm22Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SoftDM_SoftDM22");

                entity.HasOne(d => d.SoftDm25)
                    .WithMany(p => p.SoftDmrs)
                    .HasForeignKey(d => d.SoftDm25Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SoftDM_SoftDM251");

                entity.HasOne(d => d.SoftDm32)
                    .WithMany(p => p.SoftDmrs)
                    .HasForeignKey(d => d.SoftDm32Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SoftDM_SoftDM32");

                entity.HasOne(d => d.SoftDm40)
                    .WithMany(p => p.SoftDmrs)
                    .HasForeignKey(d => d.SoftDm40Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SoftDM_SoftDM40");

                entity.HasOne(d => d.SoftDm50)
                    .WithMany(p => p.SoftDmrs)
                    .HasForeignKey(d => d.SoftDm50Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SoftDM_SoftDM50");
            });

            modelBuilder.Entity<SoftDmr22>(entity =>
            {
                entity.HasKey(e => e.SoftDm22Id)
                    .HasName("PK_SoftDM22");

                entity.ToTable("SoftDMR22");

                entity.Property(e => e.SoftDm22Id).HasColumnName("softDm22_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDmr25>(entity =>
            {
                entity.HasKey(e => e.SoftDm25Id)
                    .HasName("PK_SoftDM25");

                entity.ToTable("SoftDMR25");

                entity.Property(e => e.SoftDm25Id).HasColumnName("softDm25_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDmr32>(entity =>
            {
                entity.HasKey(e => e.SoftDm32Id)
                    .HasName("PK_SoftDM32");

                entity.ToTable("SoftDMR32");

                entity.Property(e => e.SoftDm32Id).HasColumnName("softDm32_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDmr40>(entity =>
            {
                entity.HasKey(e => e.SoftDm40Id)
                    .HasName("PK_SoftDM40");

                entity.ToTable("SoftDMR40");

                entity.Property(e => e.SoftDm40Id).HasColumnName("softDm40_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDmr50>(entity =>
            {
                entity.HasKey(e => e.SoftDm50Id)
                    .HasName("PK_SoftDM50");

                entity.ToTable("SoftDMR50");

                entity.Property(e => e.SoftDm50Id).HasColumnName("softDm50_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDrl>(entity =>
            {
                entity.ToTable("SoftDRL");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HumidityH).HasColumnName("humidityH");

                entity.Property(e => e.HumidityK).HasColumnName("humidityK");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.SoftDrL22Id).HasColumnName("softDrL22_id");

                entity.Property(e => e.SoftDrL25Id).HasColumnName("softDrL25_id");

                entity.Property(e => e.SoftDrL32Id).HasColumnName("softDrL32_id");

                entity.Property(e => e.SoftDrL40Id).HasColumnName("softDrL40_id");

                entity.Property(e => e.SoftDrL50Id).HasColumnName("softDrL50_id");

                entity.HasOne(d => d.SoftDrL22)
                    .WithMany(p => p.SoftDrls)
                    .HasForeignKey(d => d.SoftDrL22Id)
                    .HasConstraintName("FK_SoftDRL_SoftDRL22");

                entity.HasOne(d => d.SoftDrL25)
                    .WithMany(p => p.SoftDrls)
                    .HasForeignKey(d => d.SoftDrL25Id)
                    .HasConstraintName("FK_SoftDRL_SoftDRL25");

                entity.HasOne(d => d.SoftDrL32)
                    .WithMany(p => p.SoftDrls)
                    .HasForeignKey(d => d.SoftDrL32Id)
                    .HasConstraintName("FK_SoftDRL_SoftDRL32");

                entity.HasOne(d => d.SoftDrL40)
                    .WithMany(p => p.SoftDrls)
                    .HasForeignKey(d => d.SoftDrL40Id)
                    .HasConstraintName("FK_SoftDRL_SoftDRL40");

                entity.HasOne(d => d.SoftDrL50)
                    .WithMany(p => p.SoftDrls)
                    .HasForeignKey(d => d.SoftDrL50Id)
                    .HasConstraintName("FK_SoftDRL_SoftDRL50");
            });

            modelBuilder.Entity<SoftDrl22>(entity =>
            {
                entity.ToTable("SoftDRL22");

                entity.Property(e => e.SoftDrL22Id).HasColumnName("softDrL22_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDrl25>(entity =>
            {
                entity.ToTable("SoftDRL25");

                entity.Property(e => e.SoftDrL25Id).HasColumnName("softDrL25_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDrl32>(entity =>
            {
                entity.ToTable("SoftDRL32");

                entity.Property(e => e.SoftDrL32Id).HasColumnName("softDrL32_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDrl40>(entity =>
            {
                entity.HasKey(e => e.SoftDrL32Id);

                entity.ToTable("SoftDRL40");

                entity.Property(e => e.SoftDrL32Id).HasColumnName("softDrL32_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<SoftDrl50>(entity =>
            {
                entity.ToTable("SoftDRL50");

                entity.Property(e => e.SoftDrL50Id).HasColumnName("softDrL50_id");

                entity.Property(e => e.DurationSteps).HasColumnName("durationSteps");

                entity.Property(e => e.Humidity)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("humidity");

                entity.Property(e => e.TempDry).HasColumnName("tempDry");
            });

            modelBuilder.Entity<ValueWood>(entity =>
            {
                entity.ToTable("ValueWood");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndWetness).HasColumnName("endWetness");

                entity.Property(e => e.EndWidth).HasColumnName("endWidth");

                entity.Property(e => e.HoursWood).HasColumnName("hoursWood");

                entity.Property(e => e.StartWetness).HasColumnName("startWetness");

                entity.Property(e => e.StartWidth).HasColumnName("startWidth");

                entity.Property(e => e.TypeWood)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("typeWood");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
