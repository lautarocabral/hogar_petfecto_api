﻿namespace hogar_petfecto_api.Models
{
    public class Suscripcion
    {
        // Atributos de la clase
        public int Id { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public double Monto { get; private set; }
        public bool Estado { get; private set; }

        // Constructor para inicializar una nueva suscripción
        public Suscripcion( DateTime fechaInicio, DateTime fechaFin, double monto, bool estado)
        {
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Monto = monto;
            Estado = estado;
        }

        // Método para actualizar el estado de la suscripción
        public void CambiarEstado(bool nuevoEstado)
        {
            Estado = nuevoEstado;
        }

        // Método para extender la fecha de finalización de la suscripción
        public void ExtenderSuscripcion(DateTime nuevaFechaFin)
        {
            if (nuevaFechaFin > FechaFin)
            {
                FechaFin = nuevaFechaFin;
            }
        }
    }

}
