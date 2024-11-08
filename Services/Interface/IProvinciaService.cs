using hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Dtos;

namespace hogar_petfecto_api.Services.Interface
{
    public interface IProvinciaService
    {
        Task<List<Localidad?>> GetLocalidades(int id);
        Task<List<Provincia>> GetProvincias();
    }
}
