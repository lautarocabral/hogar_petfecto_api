namespace alumnos_api.Models
{
    public class Materia
    {
        public Materia() { }
        public Materia(string nombre, List<Alumno> alumnos, List<Nota> notas)
        {
            Nombre = nombre;
            Alumnos = alumnos;
            Notas = notas;
        }
        public int? MateriaId { get; set; }
        public string Nombre { get; set; }
        public List<Alumno> Alumnos { get; set; }
        public List<Nota> Notas { get; set; }

    }
}
