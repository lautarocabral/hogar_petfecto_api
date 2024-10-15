namespace hogar_petfecto_api.Models
{
    public class Categoria
    {
        private Categoria()
        {
        }

        public Categoria(string nombre)
        {
            Nombre = nombre;
        }

        public int Id { get; private set; }
        public string Nombre { get; private set; }
    }

}
