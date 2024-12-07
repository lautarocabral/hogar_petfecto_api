﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace hogar_petfecto_api.Models.Dtos.Request
{
    public class RecuperarClaveRequestDto
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}