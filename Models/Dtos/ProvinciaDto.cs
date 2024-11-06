namespace hogar_petfecto_api.Models.Dtos
{
    public class ProvinciaDto
    {
        public int Id { get; set; }
        public string Provincia { get; set; }

        public LocalidadDto LocalidadDto { get; set; }
    }
}
