﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using LojaOnline.Models;

#nullable disable

namespace LojaOnline.Migrations
{
    [DbContext(typeof(CadastroLojaContext))]
    partial class CadastroLojaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Usuario", b =>
                {
                    b.Property<string>("CpfCnpj")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodigoProduto")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CpfCnpjBaixa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataSaida")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Encerrado")
                        .HasColumnType("bit");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeProduto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CpfCnpj", "CodigoProduto");

                    b.ToTable("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}