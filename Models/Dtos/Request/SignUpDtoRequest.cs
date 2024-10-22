namespace hogar_petfecto_api.Models.Dtos.Request
{
    public class SignUpDtoRequest
    {
        public string Dni { get; set; }
        public string RazonSocial { get; set; }
        public int LocalidadId { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
