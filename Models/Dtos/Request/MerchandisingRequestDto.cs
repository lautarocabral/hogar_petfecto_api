﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace hogar_petfecto_api.Models.Dtos.Request
{
    public class MerchandisingRequestDto
    {
        public int? ProductoId { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
        public int CategoriaId { get; set; }
        public string Imagen { get; set; }

    }
}
