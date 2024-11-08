using hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Dtos.Request;
using System.Security.Claims;

namespace hogar_petfecto_api.Services.Interface
{
    public interface IAuthService
    {
        string GenerarToken(Usuario usuario);
        Task<Usuario?> ValidarCredencialesAsync(string email, string contraseña);
        Task<ApiResponse<Usuario>> Login(Usuario usuario);
        Task<ApiResponse<Usuario>> SignUp(SignUpRequestDto signUpDtoRequest);
        ClaimsPrincipal ValidarToken(string token);

        ClaimsPrincipal GetClaimsPrincipalFromToken(HttpContext httpContext);
        Task<Usuario?> ReturnUsuario(string userId);
    }
}
