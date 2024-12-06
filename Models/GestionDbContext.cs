using hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Perfiles;
using hogar_petfecto_api.Models.Seguridad;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace alumnos_api.Models
{
    public class GestionDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public GestionDbContext(IConfiguration configuration) : base()
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Conectarse a SQL Server con la cadena de conexión de app settings
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Pedido
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Protectora)
                .WithMany()
                .HasForeignKey(p => p.ProtectoraId) // Asegúrate de usar la clave correcta
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de precisión para precios
            modelBuilder.Entity<LineaPedido>()
                .Property(lp => lp.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            // Configuración de Suscripción
            modelBuilder.Entity<Suscripcion>()
                .HasOne(s => s.Veterinaria)
                .WithMany(v => v.Suscripciones)
                .HasForeignKey(s => s.VeterinariaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Oferta
            modelBuilder.Entity<Oferta>()
                .HasOne(o => o.Veterinaria)
                .WithMany(v => v.Ofertas)
                .HasForeignKey(o => o.VeterinariaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración del discriminador para Perfil
            modelBuilder.Entity<Perfil>()
                .HasDiscriminator<string>("PerfilTipo")
                .HasValue<Cliente>("Cliente")
                .HasValue<Protectora>("Protectora")
                .HasValue<Veterinaria>("Veterinaria");

            modelBuilder.Entity<Mascota>()
            .HasOne(m => m.Protectora) // Relación hacia Protectora
            .WithMany(p => p.Mascotas) // Una Protectora puede tener muchas Mascotas
            .HasForeignKey(m => m.ProtectoraId) // Clave foránea explícita
            .OnDelete(DeleteBehavior.Restrict); // No borrar en cascada


            base.OnModelCreating(modelBuilder);
        }



        public DbSet<Adoptante> Adoptantes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Protectora> Protectoras { get; set; }
        public DbSet<Veterinaria> Veterinarias { get; set; }
        public DbSet<Oferta> Ofertas { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Adopcion> Adopciones { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<EstadoPostulacion> EstadosPostulacion { get; set; }
        public DbSet<LineaPedido> LineasPedido { get; set; }
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<OcCount> OcCounts { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Postulacion> Postulaciones { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Suscripcion> Suscripciones { get; set; }
        public DbSet<TipoPerfil> TiposPerfil { get; set; }
        public DbSet<TipoMascota> TiposMascota { get; set; }


    }
}
