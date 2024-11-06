namespace hogar_petfecto_api.Models.Dtos
{
    public class GrupoDto
    {
        public GrupoDto()
        {
            
        }
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public List<PermisoDto> Permisos { get; set; }
    }
}
