namespace hogar_petfecto_api.Models
{
    public class LineaPedido
    {
        private LineaPedido()
        {
        }

        public LineaPedido(decimal precio, Producto producto, int cantidad)
        {
            Precio = precio;
            Producto = producto;
            Cantidad = cantidad;
        }
        public int Id { get; private set; }
        public decimal Precio { get; private set; }
        public Producto Producto { get; private set; }
        public int Cantidad { get; private set; }
    }

}
