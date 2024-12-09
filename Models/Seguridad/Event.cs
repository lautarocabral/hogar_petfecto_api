// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace hogar_petfecto_api.Models.Seguridad
{
    public class Event
    {
        private Event()
        {

        }


        public Event(int id, string detalle, int moduloId, DateTime dateTime)
        {
            UserId = id;
            Detalle = detalle;
            ModuloId = moduloId;
            Fecha = dateTime;
        }

        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Detalle { get; private set; }
        public int ModuloId { get; private set; }
        public DateTime Fecha { get; private set; }
    }
}
