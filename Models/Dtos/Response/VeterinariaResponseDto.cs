// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using hogar_petfecto_api.Models.Perfiles;

namespace hogar_petfecto_api.Models.Dtos.Response
{
    public class VeterinariaResponseDto
    {
        public string token { get; set; }
        public List<VeterinariaDto> Veterinarias{ get; set; }
    }
}
