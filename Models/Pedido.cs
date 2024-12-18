﻿using hogar_petfecto_api.Models.Perfiles;

namespace hogar_petfecto_api.Models
{
    public class Pedido
    {
        private Pedido()
        {
        }

        public Pedido(DateTime fecha, List<LineaPedido> lineaPedido, int nroOrdenCompra, DateTime fechaOrdenCompra, string idPago, DateTime fechaPago, double monto, Cliente cliente, Protectora protectora)
        {
            Fecha = fecha;
            LineaPedido = lineaPedido ?? new List<LineaPedido>();
            NroOrdenCompra = nroOrdenCompra;
            FechaOrdenCompra = fechaOrdenCompra;
            IdPago = idPago;
            FechaPago = fechaPago;
            Monto = monto;
            Cliente = cliente;
            ClienteId = cliente.Id;
            Protectora = protectora;
            ProtectoraId = protectora.Id;
        }

        public int Id { get; private set; }
        public DateTime Fecha { get; private set; }
        public List<LineaPedido> LineaPedido { get; private set; }
        public int NroOrdenCompra { get; private set; }
        public DateTime FechaOrdenCompra { get; private set; }
        public string IdPago { get; private set; }
        public DateTime FechaPago { get; private set; }
        public double Monto { get; private set; }
        public Cliente Cliente { get; private set; }
        public int ClienteId { get; private set; }
        public Protectora Protectora { get; private set; }
        public int ProtectoraId { get; private set; }
    }

}
