using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CASTOREMPLEADO.Entity;

public partial class CastorContext : DbContext
{
    public CastorContext()
    {
    }

    public CastorContext(DbContextOptions<CastorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        // optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=castor;Username=postgres;Password=20yuver;");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cargo_pkey");

            entity.ToTable("cargo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("empleados_pkey");

            entity.ToTable("empleados");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CargoId).HasColumnName("cargo_id");
            entity.Property(e => e.Cedula).HasColumnName("cedula");
            entity.Property(e => e.FechaIngreso).HasColumnName("fecha_ingreso");
            entity.Property(e => e.Foto)
                .HasMaxLength(1000)
                .HasColumnName("foto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Cargo).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.CargoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("empleados_cargo_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
