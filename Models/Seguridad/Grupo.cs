namespace hogar_petfecto_api.Models.Seguridad
{
    public class Grupo
    {

        private Grupo()
        {

        }

        public Grupo(string descripcion, List<Permiso> permisos)
        {
            Descripcion = descripcion;
            Permisos = permisos;
        }

        public void UpdatePermisos(List<Permiso> permisos)
        {
            Permisos = permisos;
        }
        public int Id { get; private set; }
        public string Descripcion { get; private set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual List<Permiso> Permisos { get; set; }
    }

}
