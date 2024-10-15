namespace hogar_petfecto_api.Models
{
    public class EstadoPostulacion
    {
        private EstadoPostulacion()
        {
        }

        public EstadoPostulacion(string estado)
        {
            Estado = estado;
        }

        public int Id { get; private set; }
        public string Estado { get; private set; }
    }

}
