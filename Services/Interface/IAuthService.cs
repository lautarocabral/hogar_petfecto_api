using hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Dtos.Request;

namespace hogar_petfecto_api.Services.Interface
{
    public interface IAuthService
    {
        string GenerarToken(Usuario usuario);
        Task<bool> ValidarCredencialesAsync(string email, string contraseña);
        Task<ApiResponse<Usuario>> Login(Usuario usuario);
        Task<ApiResponse<Usuario>> SignUp(SignUpDtoRequest signUpDtoRequest);
    }
}
