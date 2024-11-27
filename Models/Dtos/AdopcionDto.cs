// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using hogar_petfecto_api.Models.Perfiles;

namespace hogar_petfecto_api.Models.Dtos
{
    public class AdopcionDto
    {
        public int Id { get; set; }
        public MascotaDto Mascota { get; set; }
        public AdoptanteDto Adoptante { get; set; }
        public DateTime Fecha { get; set; }
        public string Contrato { get; set; }
    }
}
