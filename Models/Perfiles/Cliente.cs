namespace hogar_petfecto_api.Models.Perfiles
{
    public class Cliente : Perfil
    {
        private Cliente() : base(default) { }
        public Cliente(TipoPerfil tipoPerfil, string cuil, string ocupacion) : base(tipoPerfil)
        {
            Cuil = cuil;
            Ocupacion = ocupacion;
        }

        public string Cuil { get; private set; }
        public string Ocupacion { get; private set; }
    }
}
