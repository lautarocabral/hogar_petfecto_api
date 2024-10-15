using alumnos_api.Models;
using hogar_petfecto_api.Models.Seguridad;

namespace hogar_petfecto_api.Services.Interface
{
    public interface ITokenService
    {
        string GenerateToken(Usuario usuario);
    }
}
