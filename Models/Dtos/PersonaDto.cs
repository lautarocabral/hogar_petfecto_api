namespace hogar_petfecto_api.Models.Dtos
{
    public class PersonaDto
    {
        public string RazonSocial { get; set; }
        public ProvinciaDto ProvinciaDto { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<PerfilDto> PerfilesDto { get; private set; }
    }
}
