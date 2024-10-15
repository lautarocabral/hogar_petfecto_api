using alumnos_api.Models;
using hogar_petfecto_api.Models.hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Perfiles;

namespace hogar_petfecto_api.Services.Interface
{
    public interface IPerfilManagerService
    {
        Task<ApiResponse<Adoptante>> CargarAdoptante(Adoptante adoptante);
        Task<ApiResponse<Cliente>> CargarCliente(Cliente cliente);
        Task<ApiResponse<Protectora>> CargarProtectora(Protectora protectora);
        Task<ApiResponse<Veterinaria>> CargarVeterinaria(Veterinaria veterinaria);
    }
}
