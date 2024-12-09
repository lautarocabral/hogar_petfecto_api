// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using hogar_petfecto_api.Models.Seguridad;

namespace hogar_petfecto_api.Models.Dtos.Response
{
    public class EventsResponseDto
    {
        public string token { get; set; }
        public List<EventDto> Events { get; set; }
    }
}
public class EventDto
{
    public EventDto(int id, string usuario, string email, string detalle, string nombremodulo, DateTime fecha)
    {
        Id = id;
        Usuario = usuario;
        Email = email;
        Detalle = detalle;
        NombreModulo = nombremodulo;
        Fecha = fecha;
    }
    public int Id { get; private set; }
    public string Usuario { get; private set; }
    public string Email { get; private set; }
    public string Detalle { get; private set; }
    public string NombreModulo { get; private set; }
    public DateTime Fecha { get; private set; }
}
