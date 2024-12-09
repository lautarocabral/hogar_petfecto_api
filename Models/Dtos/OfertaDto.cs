// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace hogar_petfecto_api.Models.Dtos
{
    public class OfertaDto
    {
        public int Id { get; set; }
        public string Producto { get; set; }
        public string Imagen { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public double Descuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Activo { get; set; }
    }
}
