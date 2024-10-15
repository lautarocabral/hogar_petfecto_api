namespace hogar_petfecto_api.Models
{
    public class LineaPedido
    {
        private LineaPedido()
        {
        }

        public LineaPedido(double precio, Producto producto, int cantidad)
        {
            Precio = precio;
            Producto = producto;
            Cantidad = cantidad;
        }

        public double Precio { get; private set; }
        public Producto Producto { get; private set; }
        public int Cantidad { get; private set; }
    }

}
