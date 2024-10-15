namespace hogar_petfecto_api.Models.Seguridad
{
    public class Usuario
    {
        public Usuario(int id, string email, string contraseña, List<Grupo> grupos)
        {
            Id = id;
            Email = email;
            Contraseña = contraseña;
            Grupos = grupos;
        }

        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Contraseña { get; private set; }
        public List<Grupo> Grupos { get; private set; }
        public Persona Persona { get; set; }
    }

}
