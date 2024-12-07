// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace hogar_petfecto_api.Models.Dtos
{
    public class OfertaDto
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
    }
}
