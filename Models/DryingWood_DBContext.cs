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
        public virtual DbSet<ValueWood> ValueWoods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-I6LAO3J\\SQLEXPRESS;Database=DryingWood_DB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Chamber>(entity =>
            {
                entity.ToTable("Chamber");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChamberNumber).HasColumnName("chamberNumber");

                entity.Property(e => e.ChamberHoursLeft).HasColumnName("chamberHoursLeft");

                entity.Property(e => e.ChamberHoursSpend).HasColumnName("chamberHoursSpend");

                entity.Property(e => e.ChamberWoodId).HasColumnName("chamberWood_id");

                entity.Property(e => e.PlanDryingId).HasColumnName("planDrying_id");

                entity.Property(e => e.ChamberCapacity).HasColumnName("chamberCapacity");

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

                entity.Property(e => e.LengValue).HasColumnName("lengValue");
                
                entity.Property(e => e.WidthValue).HasColumnName("widthValue");
                
                entity.Property(e => e.HeightValue).HasColumnName("heightValue");
                
                entity.Property(e => e.HeatCarrier).HasColumnName("heatCarrier");
                
                entity.Property(e => e.TemperatureCarrier).HasColumnName("temperatureCarrier");
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
