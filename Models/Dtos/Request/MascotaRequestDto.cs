// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace hogar_petfecto_api.Models.Dtos.Request
{
    public class MascotaRequestDto
    {

        public int TipoMascota { get; private set; }
        public string Nombre { get; private set; }
        public double Peso { get; private set; }
        public bool AptoDepto { get; private set; }
        public bool AptoPerros { get; private set; }
        public DateTime FechaNacimiento { get; private set; }
        public bool Castrado { get; private set; }
        public string Sexo { get; private set; }
        public bool Vacunado { get; private set; }
        public bool Adoptado { get; private set; }
    }
}
