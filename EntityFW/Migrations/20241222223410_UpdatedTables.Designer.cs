﻿// <auto-generated />
using System;
using EntityFW.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EntityFW.Migrations
{
    [DbContext(typeof(ConsultorioContext))]
    [Migration("20241222223410_UpdatedTables")]
    partial class UpdatedTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EntityFW.Models.Historial", b =>
                {
                    b.Property<int>("MedicoId")
                        .HasColumnType("int");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.Property<string>("Observacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicoId", "PacienteId");

                    b.HasIndex("PacienteId");

                    b.ToTable("Historiales");
                });

            modelBuilder.Entity("EntityFW.Models.Medico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Disponibilidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Especialidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Medicos");
                });

            modelBuilder.Entity("EntityFW.Models.ObraSocial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Coseguro")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ObrasSociales");
                });

            modelBuilder.Entity("EntityFW.Models.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DNI")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ObraSocialId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ObraSocialId");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("EntityFW.Models.Turno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("Horario")
                        .HasColumnType("int");

                    b.Property<int>("MedicoId")
                        .HasColumnType("int");

                    b.Property<int>("ObraSocialId")
                        .HasColumnType("int");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MedicoId")
                        .IsUnique();

                    b.HasIndex("ObraSocialId");

                    b.HasIndex("PacienteId")
                        .IsUnique();

                    b.ToTable("Turnos");
                });

            modelBuilder.Entity("EntityFW.Models.Historial", b =>
                {
                    b.HasOne("EntityFW.Models.Medico", "Medico")
                        .WithMany("Historiales")
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFW.Models.Paciente", "Paciente")
                        .WithMany("Historiales")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medico");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("EntityFW.Models.Paciente", b =>
                {
                    b.HasOne("EntityFW.Models.ObraSocial", "ObraSocial")
                        .WithMany("Pacientes")
                        .HasForeignKey("ObraSocialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ObraSocial");
                });

            modelBuilder.Entity("EntityFW.Models.Turno", b =>
                {
                    b.HasOne("EntityFW.Models.Medico", "Medico")
                        .WithOne("Turno")
                        .HasForeignKey("EntityFW.Models.Turno", "MedicoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EntityFW.Models.ObraSocial", "ObraSocial")
                        .WithMany()
                        .HasForeignKey("ObraSocialId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EntityFW.Models.Paciente", "Paciente")
                        .WithOne("Turno")
                        .HasForeignKey("EntityFW.Models.Turno", "PacienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Medico");

                    b.Navigation("ObraSocial");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("EntityFW.Models.Medico", b =>
                {
                    b.Navigation("Historiales");

                    b.Navigation("Turno");
                });

            modelBuilder.Entity("EntityFW.Models.ObraSocial", b =>
                {
                    b.Navigation("Pacientes");
                });

            modelBuilder.Entity("EntityFW.Models.Paciente", b =>
                {
                    b.Navigation("Historiales");

                    b.Navigation("Turno");
                });
#pragma warning restore 612, 618
        }
    }
}