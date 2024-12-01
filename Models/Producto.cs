using hogar_petfecto_api.Models.Perfiles;

namespace hogar_petfecto_api.Models
{
    public class Producto
    {
        private Producto()
        {
        }

        public Producto(string descripcion, int stock, decimal precio, Categoria categoria, string imagen, string titulo, Protectora protectora, int protectoraId)
        {
            Descripcion = descripcion;
            Stock = stock;
            Precio = precio;
            Categoria = categoria;
            Imagen = imagen;
            Titulo = titulo;
            Protectora = protectora;
            ProtectoraId = protectoraId;
        }

        public int Id { get; private set; }
        public string Descripcion { get; private set; }
        public string Titulo { get; private set; }

        public int Stock { get; private set; }
        public decimal Precio { get; private set; }
        public Categoria Categoria { get; private set; }
        public string Imagen { get; private set; }
        public Protectora Protectora { get; private set; }
        public int ProtectoraId { get; private set; }


        public void Update(string descripcion, int stock, decimal precio, Categoria categoria, string imagen, string titulo)
        {
            Descripcion = descripcion;
            Stock = stock;
            Precio = precio;
            Categoria = categoria;
            Imagen = imagen;
            Titulo = titulo;
        }
    }

}
