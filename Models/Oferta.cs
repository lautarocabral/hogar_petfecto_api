// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace hogar_petfecto_api.Models
{
    public class Oferta
    {
        public int Id { get; private set; }
        public string Producto { get; private set; }
        public string Imagen { get; private set; }
        public string Titulo { get; private set; }
        public string Descripcion { get; private set; }
        public double Descuento { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public bool Activo { get; private set; }

        // Constructor para inicializar la oferta
        public Oferta(int id, string producto, string imagen, string titulo, string descripcion, double descuento, DateTime fechaInicio, DateTime fechaFin, bool activo)
        {
            Id = id;
            Producto = producto;
            Imagen = imagen;
            Titulo = titulo;
            Descripcion = descripcion;
            Descuento = descuento;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Activo = activo;
        }

        // Métodos para actualizar cada propiedad
        public void SetProducto(string producto)
        {
            Producto = producto;
        }

        public void SetImagen(string imagen)
        {
            Imagen = imagen;
        }

        public void SetTitulo(string titulo)
        {
            Titulo = titulo;
        }

        public void SetDescripcion(string descripcion)
        {
            Descripcion = descripcion;
        }

        public void SetDescuento(double descuento)
        {
            Descuento = descuento;
        }

        public void SetFechaInicio(DateTime fechaInicio)
        {
            FechaInicio = fechaInicio;
        }

        public void SetFechaFin(DateTime fechaFin)
        {
            FechaFin = fechaFin;
        }

        public void SetActivo(bool activo)
        {
            Activo = activo;
        }
    }

}
