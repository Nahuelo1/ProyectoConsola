  using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoConsolaV2.Models;

public partial class ProjectConsoleContext : DbContext
{
    public ProjectConsoleContext()
    {
    }

    public ProjectConsoleContext(DbContextOptions<ProjectConsoleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Examan> Examen { get; set; }

    public virtual DbSet<ExamenPrestacion> ExamenPrestacions { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Prestacion> Prestacions { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
   //     => optionsBuilder.UseSqlServer("Server=192.168.111.9; DataBase=ProjectConsole; User=sa; Password=Sapass1?; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.ToTable("empresa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("razonSocial");
        });

        modelBuilder.Entity<Examan>(entity =>
        {
            entity.ToTable("examen");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Baja).HasColumnName("baja");
            entity.Property(e => e.Cerrado).HasColumnName("cerrado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Observacion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("observacion");
        });

        modelBuilder.Entity<ExamenPrestacion>(entity =>
        {
            entity.ToTable("examenPrestacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdExamen).HasColumnName("idExamen");
            entity.Property(e => e.IdMedico).HasColumnName("idMedico");
            entity.Property(e => e.IdPrestacion).HasColumnName("idPrestacion");

            entity.HasOne(d => d.IdExamenNavigation).WithMany(p => p.ExamenPrestacions)
                .HasForeignKey(d => d.IdExamen)
                .HasConstraintName("FK_examenPrestacion_examen");

            entity.HasOne(d => d.IdMedicoNavigation).WithMany(p => p.ExamenPrestacions)
                .HasForeignKey(d => d.IdMedico)
                .HasConstraintName("FK_examenPrestacion_medico");

            entity.HasOne(d => d.IdPrestacionNavigation).WithMany(p => p.ExamenPrestacions)
                .HasForeignKey(d => d.IdPrestacion)
                .HasConstraintName("FK_examenPrestacion_prestacion1");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.ToTable("medico");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Dni)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("dni");
            entity.Property(e => e.Especialidad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("especialidad");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Paciente");

            entity.ToTable("paciente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Dni)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("dni");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Sexo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sexo");
        });

        modelBuilder.Entity<Prestacion>(entity =>
        {
            entity.ToTable("prestacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Baja).HasColumnName("baja");
            entity.Property(e => e.Cerrado).HasColumnName("cerrado");
            entity.Property(e => e.EEnviado).HasColumnName("eEnviado");
            entity.Property(e => e.FechaCreacion).HasColumnName("fechaCreacion");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");
            entity.Property(e => e.NroPrestacion).HasColumnName("nroPrestacion");
            entity.Property(e => e.Tipo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Prestacions)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_prestacion_empresa");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Prestacions)
                .HasForeignKey(d => d.IdPaciente)
                .HasConstraintName("FK_prestacion_paciente");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
