﻿using hogar_petfecto_api.Models.Seguridad;

namespace hogar_petfecto_api.Models
{
    public class Persona
    {
        public Persona(string dni, string razonSocial, Localidad localidad, string direccion, string telefono, DateTime fechaNacimiento, List<Perfil> perfiles)
        {
            Dni = dni;
            RazonSocial = razonSocial;
            Localidad = localidad;
            Direccion = direccion;
            Telefono = telefono;
            FechaNacimiento = fechaNacimiento;
            Perfiles = perfiles;
        }

        public string Dni { get; private set; }
        public string RazonSocial { get; private set; }
        public Localidad Localidad { get; private set; }
        public string Direccion { get; private set; }
        public string Telefono { get; private set; }
        public DateTime FechaNacimiento { get; private set; }
        public List<Perfil> Perfiles { get; private set; }
        public Usuario Usuario { get; set; }
    }

}