using hogar_petfecto_api.Models.Perfiles;

namespace hogar_petfecto_api.Models
{
    public class Oferta
    {
        public int Id { get; private set; }
        public string Producto { get; private set; }
        public string Imagen { get; private set; }
        public string Titulo { get; private set; }
        public string Descripcion { get; private set; }
        public double Descuento { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public bool Activo { get; private set; }

        // Relación con Veterinaria
        public int VeterinariaId { get; private set; }
        public Veterinaria Veterinaria { get; private set; }

        private Oferta() { }

        public Oferta(
            string producto,
            string imagen,
            string titulo,
            string descripcion,
            double descuento,
            DateTime fechaInicio,
            DateTime fechaFin,
            bool activo,
            Veterinaria veterinaria)
        {
            Producto = producto;
            Imagen = imagen;
            Titulo = titulo;
            Descripcion = descripcion;
            Descuento = descuento;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Activo = activo;
            Veterinaria = veterinaria ?? throw new ArgumentNullException(nameof(veterinaria));
            VeterinariaId = veterinaria.Id;
        }

        // Método para actualizar propiedades
        public void Update(
            string producto,
            string imagen,
            string titulo,
            string descripcion,
            double descuento,
            DateTime fechaInicio,
            DateTime fechaFin,
            bool activo)
        {
            Producto = producto;
            Imagen = imagen;
            Titulo = titulo;
            Descripcion = descripcion;
            Descuento = descuento;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Activo = activo;
        }
    }
}
