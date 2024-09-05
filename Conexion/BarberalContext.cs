using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Conexion;

public partial class BarberalContext : DbContext
{
    public BarberalContext()
    {
    }

    public BarberalContext(DbContextOptions<BarberalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UsuariosBa> UsuariosBas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=RAMOS-CE;Initial Catalog=BARBERAL;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsuariosBa>(entity =>
        {
            entity.HasKey(e => e.IdBa).HasName("PK__Usuarios__8B6207E9740F471F");

            entity.ToTable("UsuariosBA");

            entity.Property(e => e.IdBa).HasColumnName("ID_BA");
            entity.Property(e => e.ApellidoM)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Apellido_M");
            entity.Property(e => e.ApellidoP)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Apellido_P");
            entity.Property(e => e.ClaveUs)
                .HasMaxLength(18)
                .IsUnicode(false)
                .HasColumnName("Clave_Us");
            entity.Property(e => e.CodigoP)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Codigo_P");
            entity.Property(e => e.Domicilio)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaBaja).HasColumnName("Fecha_Baja");
            entity.Property(e => e.FechaInicio).HasColumnName("Fecha_Inicio");
            entity.Property(e => e.FechaNacimeinto).HasColumnName("Fecha_Nacimeinto");
            entity.Property(e => e.Localidad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("localidad");
            entity.Property(e => e.ModAdm).HasColumnName("mod_adm");
            entity.Property(e => e.ModAtencion).HasColumnName("mod_atencion");
            entity.Property(e => e.ModCompras).HasColumnName("mod_compras");
            entity.Property(e => e.ModHistorico).HasColumnName("mod_historico");
            entity.Property(e => e.ModVentas).HasColumnName("mod_ventas");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Pais)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Puesto)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StatusUsuario).HasColumnName("status_usuario");
            entity.Property(e => e.UsuarioBa)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("Usuario_BA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
