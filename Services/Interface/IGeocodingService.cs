using System.Threading.Tasks;

namespace hogar_petfecto_api.Services.Interface
{
    public interface IGeocodingService
    {
        /// <summary>
        /// Obtiene la latitud y longitud a partir de una dirección proporcionada.
        /// </summary>
        /// <param name="address">Dirección a buscar.</param>
        /// <returns>Una tupla con latitud y longitud.</returns>
        Task<(double latitud, double longitud)> GetLatLongFromAddressAsync(string address);
    }
}
