using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using hogar_petfecto_api.Services.Interface;

namespace hogar_petfecto_api.Services
{
    public class GeocodingService : IGeocodingService
    {
        private readonly HttpClient _httpClient;

        public GeocodingService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<(double latitud, double longitud)> GetLatLongFromAddressAsync(string address)
        {
            // URL del servicio de Nominatim
            string requestUrl = $"https://nominatim.openstreetmap.org/search?q={address}&format=json&limit=1";

            var response = await _httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var jsonArray = JArray.Parse(json);

                if (jsonArray.Count > 0)
                {
                    var location = jsonArray[0];
                    double latitud = double.Parse(location["lat"].ToString());
                    double longitud = double.Parse(location["lon"].ToString());
                    return (latitud, longitud);
                }
                else
                {
                    throw new Exception("No se pudo encontrar la dirección ingresada.");
                }
            }
            else
            {
                throw new Exception("Error en la petición al servicio de geocodificación.");
            }
        }
    }
}
