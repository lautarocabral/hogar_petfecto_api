namespace hogar_petfecto_api.Models.Dtos
{
    public class PersonaDto
    {
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public LocalidadDto Localidad { get; set; }
        public List<PerfilDto> Perfiles { get;  set; }

        public string Dni { get; set; }

    }
}
