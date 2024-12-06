namespace hogar_petfecto_api.Models.Dtos
{
    public class UsuarioDto
    {
        public string Email { get; set; }
        public string PersonaDni { get; set; }
        public PersonaDto Persona { get; set; }
        public List<GrupoDto> Grupos { get; set; }
        public List<int> HasToUpdateProfile { get;  set; }
    }
}
