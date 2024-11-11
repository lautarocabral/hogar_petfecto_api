// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace hogar_petfecto_api.Models.Dtos
{
    public class MascotaDto
    {
        public int Id { get; set; }
        public TipoMascotaDto TipoMascota { get; set; }
        public string Nombre { get; set; }
        public double Peso { get; set; }
        public bool AptoDepto { get; set; }
        public bool AptoPerros { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Castrado { get; set; }
        public string Sexo { get; set; }
        public bool Vacunado { get; set; }
        public bool Adoptado { get; set; }
        public string Imagen { get; set; }
    }
}
