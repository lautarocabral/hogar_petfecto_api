namespace hogar_petfecto_api.Models.Dtos
{
    public class UsuarioDto
    {
        public string Email { get; set; }
        public string PersonaDni { get; set; }
        public PersonaDto PersonaDto { get; set; }
        public GrupoDto GrupoDto { get; set; }
    }
}
