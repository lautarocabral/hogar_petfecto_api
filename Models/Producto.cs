namespace hogar_petfecto_api.Models
{
    public class Producto
    {
        private Producto()
        {
        }

        public Producto(string descripcion, int stock, double precio, Categoria categoria)
        {
            Descripcion = descripcion;
            Stock = stock;
            Precio = precio;
            Categoria = categoria;
        }

        public int Id { get; private set; }
        public string Descripcion { get; private set; }
        public int Stock { get; private set; }
        public double Precio { get; private set; }
        public Categoria Categoria { get; private set; }
    }

}
