﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using alumnos_api.Models;

#nullable disable

namespace hogar_petfecto_api.Migrations
{
    [DbContext(typeof(GestionDbContext))]
    [Migration("20241209002139_UserActiveProp")]
    partial class UserActiveProp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GrupoPermiso", b =>
                {
                    b.Property<int>("GruposId")
                        .HasColumnType("int");

                    b.Property<int>("PermisosId")
                        .HasColumnType("int");

                    b.HasKey("GruposId", "PermisosId");

                    b.HasIndex("PermisosId");

                    b.ToTable("GrupoPermiso");
                });

            modelBuilder.Entity("GrupoUsuario", b =>
                {
                    b.Property<int>("GruposId")
                        .HasColumnType("int");

                    b.Property<int>("UsuariosId")
                        .HasColumnType("int");

                    b.HasKey("GruposId", "UsuariosId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("GrupoUsuario");
                });

            modelBuilder.Entity("Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HasToUpdateProfile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonaDni")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("UserActivo")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("PersonaDni")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Adopcion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdoptanteId")
                        .HasColumnType("int");

                    b.Property<string>("Contrato")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("MascotaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdoptanteId");

                    b.HasIndex("MascotaId");

                    b.ToTable("Adopciones");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.EstadoPostulacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EstadosPostulacion");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.LineaPedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int?>("PedidoId")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.HasIndex("ProductoId");

                    b.ToTable("LineasPedido");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Localidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LocalidadNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProvinciaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProvinciaId");

                    b.ToTable("Localidades");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Mascota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Adoptado")
                        .HasColumnType("bit");

                    b.Property<bool>("AptoDepto")
                        .HasColumnType("bit");

                    b.Property<bool>("AptoPerros")
                        .HasColumnType("bit");

                    b.Property<bool>("Castrado")
                        .HasColumnType("bit");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Peso")
                        .HasColumnType("float");

                    b.Property<int>("ProtectoraId")
                        .HasColumnType("int");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoMascotaId")
                        .HasColumnType("int");

                    b.Property<bool>("Vacunado")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ProtectoraId");

                    b.HasIndex("TipoMascotaId");

                    b.ToTable("Mascotas");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.OcCount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("NroOc")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("OcCounts");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Oferta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Descuento")
                        .HasColumnType("float");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Producto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VeterinariaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VeterinariaId");

                    b.ToTable("Ofertas");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaOrdenCompra")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdPago")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Monto")
                        .HasColumnType("float");

                    b.Property<int>("NroOrdenCompra")
                        .HasColumnType("int");

                    b.Property<int>("ProtectoraId")
                        .HasColumnType("int");

                    b.Property<int?>("ProtectoraId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("ProtectoraId");

                    b.HasIndex("ProtectoraId1");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Perfil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PerfilTipo")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("PersonaDni")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TipoPerfilId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonaDni");

                    b.HasIndex("TipoPerfilId");

                    b.ToTable("Perfil");

                    b.HasDiscriminator<string>("PerfilTipo").HasValue("Perfil");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Persona", b =>
                {
                    b.Property<string>("Dni")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<int>("LocalidadId")
                        .HasColumnType("int");

                    b.Property<string>("RazonSocial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Dni");

                    b.HasIndex("LocalidadId");

                    b.ToTable("Personas");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Postulacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdoptanteId")
                        .HasColumnType("int");

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("MascotaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdoptanteId");

                    b.HasIndex("EstadoId");

                    b.HasIndex("MascotaId");

                    b.ToTable("Postulaciones");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Precio")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProtectoraId")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("ProtectoraId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Provincia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ProvinciaNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Provincias");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Seguridad.Grupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Grupos");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Seguridad.Permiso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombrePermiso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permisos");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Suscripcion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<double>("Monto")
                        .HasColumnType("float");

                    b.Property<int>("TipoPlan")
                        .HasColumnType("int");

                    b.Property<int>("VeterinariaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VeterinariaId");

                    b.ToTable("Suscripciones");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.TipoMascota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TiposMascota");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.TipoPerfil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TiposPerfil");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Perfiles.Adoptante", b =>
                {
                    b.HasBaseType("hogar_petfecto_api.Models.Perfil");

                    b.Property<string>("EstadoCivil")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ExperienciaMascotas")
                        .HasColumnType("bit");

                    b.Property<int>("NroMascotas")
                        .HasColumnType("int");

                    b.Property<string>("Ocupacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Adoptante");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Perfiles.Cliente", b =>
                {
                    b.HasBaseType("hogar_petfecto_api.Models.Perfil");

                    b.Property<string>("Cuil")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ocupacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Perfil", t =>
                        {
                            t.Property("Ocupacion")
                                .HasColumnName("Cliente_Ocupacion");
                        });

                    b.HasDiscriminator().HasValue("Cliente");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Perfiles.Protectora", b =>
                {
                    b.HasBaseType("hogar_petfecto_api.Models.Perfil");

                    b.Property<int>("CantidadInicialMascotas")
                        .HasColumnType("int");

                    b.Property<int>("Capacidad")
                        .HasColumnType("int");

                    b.Property<int>("NroVoluntarios")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Protectora");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Perfiles.Veterinaria", b =>
                {
                    b.HasBaseType("hogar_petfecto_api.Models.Perfil");

                    b.Property<string>("DireccionLocal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Latitud")
                        .HasColumnType("float");

                    b.Property<double>("Longitud")
                        .HasColumnType("float");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Veterinaria");
                });

            modelBuilder.Entity("GrupoPermiso", b =>
                {
                    b.HasOne("hogar_petfecto_api.Models.Seguridad.Grupo", null)
                        .WithMany()
                        .HasForeignKey("GruposId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("hogar_petfecto_api.Models.Seguridad.Permiso", null)
                        .WithMany()
                        .HasForeignKey("PermisosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GrupoUsuario", b =>
                {
                    b.HasOne("hogar_petfecto_api.Models.Seguridad.Grupo", null)
                        .WithMany()
                        .HasForeignKey("GruposId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuariosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Usuario", b =>
                {
                    b.HasOne("hogar_petfecto_api.Models.Persona", "Persona")
                        .WithOne("Usuario")
                        .HasForeignKey("Usuario", "PersonaDni")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Adopcion", b =>
                {
                    b.HasOne("hogar_petfecto_api.Models.Perfiles.Adoptante", "Adoptante")
                        .WithMany()
                        .HasForeignKey("AdoptanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("hogar_petfecto_api.Models.Mascota", "Mascota")
                        .WithMany()
                        .HasForeignKey("MascotaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adoptante");

                    b.Navigation("Mascota");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.LineaPedido", b =>
                {
                    b.HasOne("hogar_petfecto_api.Models.Pedido", null)
                        .WithMany("LineaPedido")
                        .HasForeignKey("PedidoId");

                    b.HasOne("hogar_petfecto_api.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Localidad", b =>
                {
                    b.HasOne("hogar_petfecto_api.Models.Provincia", "Provincia")
                        .WithMany("Localidades")
                        .HasForeignKey("ProvinciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provincia");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Mascota", b =>
                {
                    b.HasOne("hogar_petfecto_api.Models.Perfiles.Protectora", "Protectora")
                        .WithMany("Mascotas")
                        .HasForeignKey("ProtectoraId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("hogar_petfecto_api.Models.TipoMascota", "TipoMascota")
                        .WithMany()
                        .HasForeignKey("TipoMascotaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Protectora");

                    b.Navigation("TipoMascota");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Oferta", b =>
                {
                    b.HasOne("hogar_petfecto_api.Models.Perfiles.Veterinaria", "Veterinaria")
                        .WithMany("Ofertas")
                        .HasForeignKey("VeterinariaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Veterinaria");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Pedido", b =>
                {
                    b.HasOne("hogar_petfecto_api.Models.Perfiles.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("hogar_petfecto_api.Models.Perfiles.Protectora", "Protectora")
                        .WithMany()
                        .HasForeignKey("ProtectoraId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("hogar_petfecto_api.Models.Perfiles.Protectora", null)
                        .WithMany("Pedidos")
                        .HasForeignKey("ProtectoraId1");

                    b.Navigation("Cliente");

                    b.Navigation("Protectora");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Perfil", b =>
                {
                    b.HasOne("hogar_petfecto_api.Models.Persona", "Persona")
                        .WithMany("Perfiles")
                        .HasForeignKey("PersonaDni")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("hogar_petfecto_api.Models.TipoPerfil", "TipoPerfil")
                        .WithMany()
                        .HasForeignKey("TipoPerfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");

                    b.Navigation("TipoPerfil");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Persona", b =>
                {
                    b.HasOne("hogar_petfecto_api.Models.Localidad", "Localidad")
                        .WithMany()
                        .HasForeignKey("LocalidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Localidad");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Postulacion", b =>
                {
                    b.HasOne("hogar_petfecto_api.Models.Perfiles.Adoptante", "Adoptante")
                        .WithMany()
                        .HasForeignKey("AdoptanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("hogar_petfecto_api.Models.EstadoPostulacion", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("hogar_petfecto_api.Models.Mascota", "Mascota")
                        .WithMany()
                        .HasForeignKey("MascotaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adoptante");

                    b.Navigation("Estado");

                    b.Navigation("Mascota");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Producto", b =>
                {
                    b.HasOne("hogar_petfecto_api.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("hogar_petfecto_api.Models.Perfiles.Protectora", "Protectora")
                        .WithMany("Productos")
                        .HasForeignKey("ProtectoraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Protectora");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Suscripcion", b =>
                {
                    b.HasOne("hogar_petfecto_api.Models.Perfiles.Veterinaria", "Veterinaria")
                        .WithMany("Suscripciones")
                        .HasForeignKey("VeterinariaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Veterinaria");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Pedido", b =>
                {
                    b.Navigation("LineaPedido");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Persona", b =>
                {
                    b.Navigation("Perfiles");

                    b.Navigation("Usuario")
                        .IsRequired();
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Provincia", b =>
                {
                    b.Navigation("Localidades");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Perfiles.Protectora", b =>
                {
                    b.Navigation("Mascotas");

                    b.Navigation("Pedidos");

                    b.Navigation("Productos");
                });

            modelBuilder.Entity("hogar_petfecto_api.Models.Perfiles.Veterinaria", b =>
                {
                    b.Navigation("Ofertas");

                    b.Navigation("Suscripciones");
                });
#pragma warning restore 612, 618
        }
    }
}
