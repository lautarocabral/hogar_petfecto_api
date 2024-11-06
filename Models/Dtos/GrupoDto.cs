namespace hogar_petfecto_api.Models.Dtos
{
    public class GrupoDto
    {
        public int Id { get; set; }
        public string GrupoNombre { get; set; }
        public List<PermisoDto> PermisoDtos { get; set; }
    }
}
