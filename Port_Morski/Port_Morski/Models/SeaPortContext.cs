using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Port_Morski.Models;

public partial class SeaPortContext : DbContext
{
    public SeaPortContext()
    {
    }

    public SeaPortContext(DbContextOptions<SeaPortContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Dock> Docks { get; set; }

    public virtual DbSet<EmpSchedule> EmpSchedules { get; set; }

    public virtual DbSet<Ship> Ships { get; set; }

    public virtual DbSet<ShipSchedule> ShipSchedules { get; set; }

    public virtual DbSet<Operations> Operationss { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public object Cargo { get; internal set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=SeaPort;Trusted_Connection=False;", options => options.EnableRetryOnFailure());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.ToTable("Cargo");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ShipId).HasColumnName("ship_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Ship).WithMany(p => p.Cargos)
                .HasForeignKey(d => d.ShipId)
                .HasConstraintName("FK_Cargo_Ships");
        });

        modelBuilder.Entity<Dock>(entity =>
        {
            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<EmpSchedule>(entity =>
        {
            entity.ToTable("EmpSchedule");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.ShiftType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("shift_type");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.EmpSchedules)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_EmpSchedule_Users");
        });

        modelBuilder.Entity<Ship>(entity =>
        {
            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<ShipSchedule>(entity =>
        {
            entity.ToTable("ShipSchedule");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.ArriveDate)
                .HasColumnType("datetime")
                .HasColumnName("arrive_date");
            entity.Property(e => e.DockId).HasColumnName("dock_id");
            entity.Property(e => e.FlowOutDate)
                .HasColumnType("datetime")
                .HasColumnName("flow_out_date");
            entity.Property(e => e.ShipId).HasColumnName("ship_id");

            entity.HasOne(d => d.Dock).WithMany(p => p.ShipSchedules)
                .HasForeignKey(d => d.DockId)
                .HasConstraintName("FK_ShipSchedule_Docks");

            entity.HasOne(d => d.Ship).WithMany(p => p.ShipSchedules)
                .HasForeignKey(d => d.ShipId)
                .HasConstraintName("FK_ShipSchedule_Ships");
        });

        modelBuilder.Entity<Operations>(entity =>
        {
            entity.ToTable("Operations");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.ShipId).HasColumnName("dock_id");
            entity.Property(e => e.DockId).HasColumnName("ship_id");
            entity.Property(e => e.Approved).HasColumnName("approved");

            entity.HasOne(d => d.Dock).WithMany(p => p.Operationss)
                .HasForeignKey(d => d.DockId)
                .HasConstraintName("FK_Operations_Docks");

            entity.HasOne(d => d.Ship).WithMany(p => p.Operationss)
                .HasForeignKey(d => d.ShipId)
                .HasConstraintName("FK_Operations_Ships");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UserRole)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
