namespace hogar_petfecto_api.Models.Perfiles
{
    public class Protectora : Perfil
    {
        private Protectora() : base(default) { }
        public Protectora(TipoPerfil tipoPerfil, int capacidad, int nroVoluntarios, List<Pedido> pedidos, List<Producto> productos, List<Mascota> mascotas, int cantidadInicialMascotas)
            : base(tipoPerfil) // Llama al constructor de Perfil
        {
            Capacidad = capacidad;
            NroVoluntarios = nroVoluntarios;
            Pedidos = pedidos ?? new List<Pedido>();
            Productos = productos ?? new List<Producto>();
            Mascotas = mascotas ?? new List<Mascota>();
            CantidadInicialMascotas = cantidadInicialMascotas;
        }

        public int Capacidad { get; private set; }
        public int NroVoluntarios { get; private set; }
        public List<Pedido> Pedidos { get; private set; }
        public List<Producto> Productos { get; private set; }
        public List<Mascota> Mascotas { get; private set; }
        public int CantidadInicialMascotas { get; private set; }
    }
}
