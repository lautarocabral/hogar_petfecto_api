namespace hogar_petfecto_api.Models.Dtos
{
    public class LocalidadDto
    {
        public int Id { get; set; }
        public string LocalidadNombre { get; set; }
        public ProvinciaDto Provincia { get; set; }
    }
}
