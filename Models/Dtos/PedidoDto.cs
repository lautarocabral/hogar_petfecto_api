// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using hogar_petfecto_api.Models.Perfiles;

namespace hogar_petfecto_api.Models.Dtos
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public List<LineaPedidoDto> LineaPedido { get; set; }
        public int NroOrdenCompra { get; set; }
        public DateTime FechaOrdenCompra { get; set; }
        public string IdPago { get; set; }
        public DateTime FechaPago { get; set; }
        public double Monto { get; set; }
        public ClienteDto Cliente { get; set; }
        public ProtectoraDto Protectora { get; set; }
    }
}
