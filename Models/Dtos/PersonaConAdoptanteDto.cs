// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace hogar_petfecto_api.Models.Dtos
{
    public class PersonaConAdoptanteDto
    {
        public string Dni { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string EstadoCivil { get; set; }
        public string Ocupacion { get; set; }
        public bool ExperienciaMascotas { get; set; }
        public int NroMascotas { get; set; }
        public int AdoptanteId { get; set; }
    }

}
