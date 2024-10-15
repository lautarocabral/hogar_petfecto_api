namespace hogar_petfecto_api.Models.Seguridad
{
    public class Grupo
    {
        public Grupo(int id, string grupoNombre)
        {
            Id = id;
            GrupoNombre = grupoNombre;
        }

        public int Id { get; private set; }
        public string GrupoNombre { get; private set; }
    }

}
