namespace alumnos_api.Models
{
    public class Nota
    {

        public Nota() { }
        public Nota(int calificacion)
        {
            Calificacion = calificacion;
        }
        public int NotaId { get; set; }
        public int Calificacion { get; set; }


    }
}
