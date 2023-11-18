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

    public virtual DbSet<Terminal> Terminals { get; set; }

    public virtual DbSet<Magazine> Magazines { get; set; }

    public virtual DbSet<Dock> Docks { get; set; }

    public virtual DbSet<EmpSchedule> EmpSchedules { get; set; }

    public virtual DbSet<Ship> Ships { get; set; }

    public virtual DbSet<ShipSchedule> ShipSchedules { get; set; }

    public virtual DbSet<Operacje> Operacje { get; set; }

    public virtual DbSet<Operacje_Log> Operacje_Logs { get; set; }

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

        modelBuilder.Entity<Operacje>(entity =>
        {
            entity.ToTable("Operacje");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.Operation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("operation");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Approved)
                .HasColumnType("bool")
                .HasColumnName("approved");
            entity.Property(e => e.DockId).HasColumnName("dock_id");
            entity.Property(e => e.ShipId).HasColumnName("ship_id");

            entity.HasOne(d => d.Dock).WithMany(p => p.Operacje)
                .HasForeignKey(d => d.DockId)
                .HasConstraintName("FK_Operacje_Docks");

            entity.HasOne(d => d.Ship).WithMany(p => p.Operacje)
                .HasForeignKey(d => d.ShipId)
                .HasConstraintName("FK_Operacje_Ships");
        });

        modelBuilder.Entity<Operations>(entity =>
        {
            entity.ToTable("Operations");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.Operation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("operation");
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

        modelBuilder.Entity<Terminal>(entity =>
        {
            entity.ToTable("Terminals");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");
            entity.Property(e => e.MaxCapacity)
                .HasColumnName("max_capacity");
            entity.Property(e => e.Available)
                .HasColumnType("bool")
                .HasColumnName("available");
            entity.Property(e => e.AvailableFromDate)
                .HasColumnType("datetime")
                .HasColumnName("available_from_date");
            entity.Property(e => e.DockId).HasColumnName("dock_id");

            entity.HasOne(d => d.Dock).WithMany(p => p.Terminals)
                .HasForeignKey(d => d.DockId)
                .HasConstraintName("FK_Terminals_Docks");
        });

        modelBuilder.Entity<Magazine>(entity =>
        {
            entity.ToTable("Magazines");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Area)
                .HasColumnName("area");
            entity.Property(e => e.AvailableCapacity)
                .HasColumnName("available_capacity");
            entity.Property(e => e.Specification)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("specification");
            entity.Property(e => e.DockId).HasColumnName("dock_id");

            entity.HasOne(d => d.Dock).WithMany(p => p.Magazines)
                .HasForeignKey(d => d.DockId)
                .HasConstraintName("FK_Magazines_Docks");
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


        modelBuilder.Entity<Operacje_Log>(entity =>
        {
            entity.ToTable("Operacje_Log"); // Ustaw nazwę tabeli

            entity.Property(e => e.LogID)
                .HasColumnName("LogID")
                .IsRequired()
                .UseIdentityColumn();

            entity.Property(e => e.OldOperation)
                .HasColumnName("OldOperation")
                .HasColumnType("nvarchar(max)");

            entity.Property(e => e.NewOperation)
                .HasColumnName("NewOperation")
                .HasColumnType("nvarchar(max)");

            // Dodaj konfiguracje dla pozostałych właściwości
            entity.Property(e => e.OldShipId)
                .HasColumnName("OldShipId")
                .HasColumnType("int");

            entity.Property(e => e.NewShipId)
                .HasColumnName("NewShipId")
                .HasColumnType("int");

            entity.Property(e => e.OldDockId)
                .HasColumnName("OldDockId")
                .HasColumnType("int");

            entity.Property(e => e.NewDockId)
                .HasColumnName("NewDockId")
                .HasColumnType("int");

            entity.Property(e => e.OldApproved)
                .HasColumnName("OldApproved")
                .HasColumnType("bit");

            entity.Property(e => e.NewApproved)
                .HasColumnName("NewApproved")
                .HasColumnType("bit");

            entity.Property(e => e.OldDate)
                .HasColumnName("OldDate")
                .HasColumnType("datetime");

            entity.Property(e => e.NewDate)
                .HasColumnName("NewDate")
                .HasColumnType("datetime");

            entity.Property(e => e.OperationTypeOnTable)
                .HasColumnName("OperationTypeOnTable")
                .HasColumnType("nvarchar(50)");

            entity.Property(e => e.LogDate)
                .HasColumnName("LogDate")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            entity.HasKey(e => e.LogID);

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
