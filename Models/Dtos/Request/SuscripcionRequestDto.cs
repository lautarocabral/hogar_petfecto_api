// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace hogar_petfecto_api.Models.Dtos.Request
{
    public class SuscripcionRequestDto
    {
        public int suscripcionId { get; set; }
        public TipoPlan tipoPlan { get; set; }
        public bool estado { get; set; }
    }
}
