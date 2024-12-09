namespace hogar_petfecto_api.Models.Perfiles
{
    public class Cliente : Perfil
    {
        private Cliente() : base() { }

        public Cliente(TipoPerfil tipoPerfil, string cuil, string ocupacion, Persona persona)
            : base(tipoPerfil, persona)
        {
            Cuil = cuil;
            Ocupacion = ocupacion;
        }

        public string Cuil { get; private set; }
        public string Ocupacion { get; private set; }

        public void UpdateCliente(string cuil, string ocupacion)
        {
            Cuil = cuil;
            Ocupacion = ocupacion;
        }
    }
}
