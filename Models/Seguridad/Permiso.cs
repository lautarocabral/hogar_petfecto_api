namespace hogar_petfecto_api.Models.Seguridad
{
    public class Permiso
    {
        public int Id { get; private set; }
        public string PermisoNombre { get; private set; }

        public Permiso(int id, string permisoNombre)
        {
            Id = id;
            PermisoNombre = permisoNombre;
        }


        public void CambiarNombre(string nuevoNombre)
        {
            if (!string.IsNullOrEmpty(nuevoNombre))
            {
                PermisoNombre = nuevoNombre;
            }
        }
    }

}
