// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace hogar_petfecto_api.Models.Dtos.Request
{
    public class PedidoRequestDto
    {
        public List<AltaPedido> Pedidos { get; set; } = new();
    }

    public class AltaPedido
    {
        public int ProtectoraId { get; set; }
        public List<AltaId> Productos { get; set; } = new();
    }

    public class AltaId
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
    }
}
