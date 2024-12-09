namespace hogar_petfecto_api.Models.Perfiles
{
    public class Protectora : Perfil
    {
        private Protectora() : base() { }

        public Protectora(
            TipoPerfil tipoPerfil,
            int capacidad,
            int nroVoluntarios,
            List<Pedido> pedidos,
            List<Producto> productos,
            List<Mascota> mascotas,
            int cantidadInicialMascotas, Persona persona)
            : base(tipoPerfil, persona)
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

        // Method to add a new Mascota to the list
        public void AddMascota(Mascota mascota)
        {
            if (Mascotas == null)
            {
                Mascotas = new List<Mascota>();
            }
            Mascotas.Add(mascota);
        }

        public void UpdateProtectora(int capacidad, int nroVoluntarios, List<Pedido> pedidos, List<Producto> productos, List<Mascota> mascotas, int cantidadInicialMascotas)
        {
            Capacidad = capacidad;
            NroVoluntarios = nroVoluntarios;
            Pedidos = pedidos;
            Productos = productos;
            Mascotas = mascotas;
            CantidadInicialMascotas = cantidadInicialMascotas;
        }
    }
}
