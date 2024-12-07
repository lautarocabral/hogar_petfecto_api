namespace hogar_petfecto_api.Models.Dtos
{
    public class VeterinariaDto
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public List<SuscripcionDto> Suscripciones { get; set; }
        public string DireccionLocal { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }

        public List<OfertaDto> Ofertas { get; set; }
    }
}
