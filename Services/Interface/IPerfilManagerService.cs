using alumnos_api.Models;
using hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Dtos;
using hogar_petfecto_api.Models.Perfiles;

namespace hogar_petfecto_api.Services.Interface
{
    public interface IPerfilManagerService
    {
        Task<Usuario?> CargarAdoptante(AdoptanteDto adoptanteDto, int userId);
        Task<Usuario?> CargarProtectora(ProtectoraDto protectoraDto, int userId);
        Task<Usuario?> CargarCliente(ClienteDto clienteDto, int userId);
        Task<Usuario?> CargarVeterinaria(VeterinariaDto veterinariaDto, int userId);
    }
}
