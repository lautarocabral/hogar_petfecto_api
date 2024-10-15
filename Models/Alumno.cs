namespace alumnos_api.Models
{
    public class Alumno
    {
        public Alumno() { }
        public int? AlumnoId { get; set; }
        public string Nombre { get; set; }
        public List<Materia> Materias { get; set; }

        public Alumno(List<Materia> materias, string nombre)
        {
            Materias = materias;
            Nombre = nombre;
        }
    }
}
