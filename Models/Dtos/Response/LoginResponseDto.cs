namespace hogar_petfecto_api.Models.Dtos.Response
{
    public class LoginResponseDto
    {
        public string token { get; set; }
        public UsuarioDto UsuarioResponseDto { get; set; }
    }
}
