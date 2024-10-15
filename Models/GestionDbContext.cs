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
        }

        public DbSet<Adoptante> Adoptantes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Protectora> Protectoras { get; set; }
        public DbSet<Veterinaria> Veterinarias { get; set; }
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
