using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SegurosLafiseBackend.Entities;

public partial class SegurosDbContext : DbContext
{
    public SegurosDbContext()
    {
    }

    public SegurosDbContext(DbContextOptions<SegurosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Coverage> Coverages { get; set; }

    public virtual DbSet<CoverageCategory> CoverageCategories { get; set; }

    public virtual DbSet<InsurancePolicy> InsurancePolicies { get; set; }

    public virtual DbSet<InsurancePolicyCoverage> InsurancePolicyCoverages { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=SegurosLafiseBD;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Client__3213E83F7940B7B0");

            entity.ToTable("Client");

            entity.HasIndex(e => e.Identification, "UQ__Client__AAA7C1F5DB11CD92").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.DeleteAt)
                .HasColumnType("datetime")
                .HasColumnName("deleteAt");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Identification)
                .HasMaxLength(50)
                .HasColumnName("identification");
            entity.Property(e => e.NameClient)
                .HasMaxLength(100)
                .HasColumnName("nameClient");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("updateAt");
        });

        modelBuilder.Entity<Coverage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coverage__3213E83FE9B6813C");

            entity.ToTable("Coverage");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.DeleteAt)
                .HasColumnType("datetime")
                .HasColumnName("deleteAt");
            entity.Property(e => e.IdCoverageCategory).HasColumnName("idCoverageCategory");
            entity.Property(e => e.Rate)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("rate");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("updateAt");

            entity.HasOne(d => d.IdCoverageCategoryNavigation).WithMany(p => p.Coverages)
                .HasForeignKey(d => d.IdCoverageCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Coverage_CoverageCategory");
        });

        modelBuilder.Entity<CoverageCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coverage__3213E83FC7E8BE5B");

            entity.ToTable("CoverageCategory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.DeleteAt)
                .HasColumnType("datetime")
                .HasColumnName("deleteAt");
            entity.Property(e => e.NameCategory)
                .HasMaxLength(65)
                .IsUnicode(false)
                .HasColumnName("nameCategory");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("updateAt");
        });

        modelBuilder.Entity<InsurancePolicy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__3213E83F83544429");

            entity.ToTable("InsurancePolicy");

            entity.HasIndex(e => e.InsurancePolicy1, "UQ__Insuranc__486D2128A794E2D4").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CoverageAmount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("coverageAmount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.DeleteAt)
                .HasColumnType("datetime")
                .HasColumnName("deleteAt");
            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.IdVehicle).HasColumnName("idVehicle");
            entity.Property(e => e.InsurancePolicy1)
                .HasMaxLength(20)
                .HasColumnName("insurancePolicy");
            entity.Property(e => e.IssueDate).HasColumnName("issueDate");
            entity.Property(e => e.TotalPremium)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("totalPremium");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("updateAt");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.InsurancePolicies)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsurancePolicy_Client");

            entity.HasOne(d => d.IdVehicleNavigation).WithMany(p => p.InsurancePolicies)
                .HasForeignKey(d => d.IdVehicle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsurancePolicy_Vehicle");
        });

        modelBuilder.Entity<InsurancePolicyCoverage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__3213E83F8E54FAC8");

            entity.ToTable("InsurancePolicyCoverage");

            entity.HasIndex(e => new { e.IdPolicy, e.IdCoverage }, "UQ_InsurancePolicy_Coverage").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.AppliedCoverageAmount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("appliedCoverageAmount");
            entity.Property(e => e.AppliedRate)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("appliedRate");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.DeleteAt)
                .HasColumnType("datetime")
                .HasColumnName("deleteAt");
            entity.Property(e => e.IdCoverage).HasColumnName("idCoverage");
            entity.Property(e => e.IdPolicy).HasColumnName("idPolicy");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("updateAt");

            entity.HasOne(d => d.IdCoverageNavigation).WithMany(p => p.InsurancePolicyCoverages)
                .HasForeignKey(d => d.IdCoverage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsurancePolicyCoverage_Coverage");

            entity.HasOne(d => d.IdPolicyNavigation).WithMany(p => p.InsurancePolicyCoverages)
                .HasForeignKey(d => d.IdPolicy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InsurancePolicyCoverage_InsurancePolicy");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vehicle__3213E83FC447CACF");

            entity.ToTable("Vehicle");

            entity.HasIndex(e => e.LicensePlate, "UQ__Vehicle__5BC9DE415D1B0009").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .HasColumnName("brand");
            entity.Property(e => e.CommercialValue)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("commercialValue");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.DeleteAt)
                .HasColumnType("datetime")
                .HasColumnName("deleteAt");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(25)
                .HasColumnName("licensePlate");
            entity.Property(e => e.ManufacturingYear).HasColumnName("manufacturingYear");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("model");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("updateAt");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
