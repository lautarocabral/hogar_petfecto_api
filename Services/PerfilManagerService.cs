using alumnos_api.Models;
using hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Dtos;
using hogar_petfecto_api.Models.Perfiles;
using hogar_petfecto_api.Models.Seguridad;
using hogar_petfecto_api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace hogar_petfecto_api.Services
{
    public class PerfilManagerService : IPerfilManagerService
    {
        private readonly GestionDbContext _context;

        public PerfilManagerService(GestionDbContext context)
        {
            _context = context;
        }
        public async Task<Usuario?> CargarAdoptante(AdoptanteDto adoptanteDto, int userId)
        {

            try
            {
                TipoPerfil tipoPerfilAdoptante = await _context.TiposPerfil.FirstOrDefaultAsync(p => p.Descripcion == "Adoptante");


                Adoptante newAdoptante = new Adoptante(tipoPerfilAdoptante, adoptanteDto.EstadoCivil, adoptanteDto.Ocupacion,
                    adoptanteDto.ExperienciaMascotas, adoptanteDto.NroMascotas);

                var grupo = await _context.Grupos.Include(g => g.Permisos).FirstOrDefaultAsync(g => g.Id == 3); // el usuario se registra con id 2 que corresponde a ADOPTANTE
                if (grupo == null)
                {
                    throw new KeyNotFoundException("Grupo no encontrado.");
                }

                List<Grupo> grupos = new List<Grupo> { grupo };

                var usuarioExistente = await _context.Usuarios
                                            .Include(u => u.Persona)
                                                .ThenInclude(p => p.Localidad)
                                                    .ThenInclude(l => l.Provincia)
                                            .Include(u => u.Persona)
                                                .ThenInclude(p => p.Perfiles)
                                                    .ThenInclude(perfil => perfil.TipoPerfil)
                                            .Include(u => u.Grupos)
                                                .ThenInclude(g => g.Permisos)
                                            .FirstOrDefaultAsync(u => u.Id == userId); // seteo el perfil al usuario que se cargo como adoptante

                usuarioExistente.Persona.Perfiles.Add(newAdoptante);


                usuarioExistente.Grupos.Add(grupo);


                if (usuarioExistente.Persona.Perfiles.Count == 4)
                {
                    usuarioExistente.Grupos.RemoveAll(g => g.Id == 2); // le quito el rol invitado si ya tiene los 4 perfiles cargados
                }
                await _context.SaveChangesAsync();

                return usuarioExistente;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Usuario?> CargarCliente(ClienteDto clienteDto, int userId)
        {
            try
            {
                TipoPerfil tipoPerfilCliente = await _context.TiposPerfil.FirstOrDefaultAsync(p => p.Descripcion == "Cliente");

                Cliente newCliente = new Cliente(tipoPerfilCliente, clienteDto.Cuil, clienteDto.Ocupacion);

                var grupo = await _context.Grupos.Include(g => g.Permisos).FirstOrDefaultAsync(g => g.Id == 4); // el usuario se registra con id 2 que corresponde a CLIENTE
                if (grupo == null)
                {
                    throw new KeyNotFoundException("Grupo no encontrado.");
                }
                List<Grupo> grupos = new List<Grupo> { grupo };

                var usuarioExistente = await _context.Usuarios
                                            .Include(u => u.Persona)
                                                .ThenInclude(p => p.Localidad)
                                                    .ThenInclude(l => l.Provincia)
                                            .Include(u => u.Persona)
                                                .ThenInclude(p => p.Perfiles)
                                                    .ThenInclude(perfil => perfil.TipoPerfil)
                                            .Include(u => u.Grupos)
                                                .ThenInclude(g => g.Permisos)
                                            .FirstOrDefaultAsync(u => u.Id == userId); // seteo el perfil al usuario que se cargo como CLIENTE

                usuarioExistente.Persona.Perfiles.Add(newCliente);


                usuarioExistente.Grupos.Add(grupo);


                if (usuarioExistente.Persona.Perfiles.Count == 4)
                {
                    usuarioExistente.Grupos.RemoveAll(g => g.Id == 2); // le quito el rol invitado si ya tiene los 4 perfiles cargados
                }
                await _context.SaveChangesAsync();

                return usuarioExistente;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Usuario?> CargarProtectora(ProtectoraDto protectoraDto, int userId)
        {
            try
            {
                TipoPerfil tipoPerfilProtectora = await _context.TiposPerfil.FirstOrDefaultAsync(p => p.Descripcion == "Protectora");

                Protectora newProtectora = new Protectora(tipoPerfilProtectora, protectoraDto.Capacidad, protectoraDto.NroVoluntarios, new List<Pedido>(), new List<Producto>(), new List<Mascota>(), protectoraDto.CantidadInicialMascotas);

                var grupo = await _context.Grupos.Include(g => g.Permisos).FirstOrDefaultAsync(g => g.Id == 6); // el usuario se registra con id 2 que corresponde a Protectora
                if (grupo == null)
                {
                    throw new KeyNotFoundException("Grupo no encontrado.");
                }
                List<Grupo> grupos = new List<Grupo> { grupo };

                var usuarioExistente = await _context.Usuarios
                                            .Include(u => u.Persona)
                                                .ThenInclude(p => p.Localidad)
                                                    .ThenInclude(l => l.Provincia)
                                            .Include(u => u.Persona)
                                                .ThenInclude(p => p.Perfiles)
                                                    .ThenInclude(perfil => perfil.TipoPerfil)
                                            .Include(u => u.Grupos)
                                                .ThenInclude(g => g.Permisos)
                                            .FirstOrDefaultAsync(u => u.Id == userId); // seteo el perfil al usuario que se cargo como Protectora

                usuarioExistente.Persona.Perfiles.Add(newProtectora);


                usuarioExistente.Grupos.Add(grupo);


                if (usuarioExistente.Persona.Perfiles.Count == 4)
                {
                    usuarioExistente.Grupos.RemoveAll(g => g.Id == 2); // le quito el rol invitado si ya tiene los 4 perfiles cargados
                }
                await _context.SaveChangesAsync();

                return usuarioExistente;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Usuario?> CargarVeterinaria(VeterinariaDto veterinariaDto, int userId)
        {
            try
            {
                TipoPerfil tipoPerfilVeterinaria = await _context.TiposPerfil.FirstOrDefaultAsync(p => p.Descripcion == "Veterinaria");

                List<Suscripcion> suscripciones = new List<Suscripcion>();
                suscripciones.Add(new Suscripcion(veterinariaDto.Suscripciones[0].FechaInicio, veterinariaDto.Suscripciones[0].FechaFin, veterinariaDto.Suscripciones[0].Monto, true));

                Veterinaria newVeterinaria = new Veterinaria(tipoPerfilVeterinaria, veterinariaDto.Latitud, veterinariaDto.Longitud, suscripciones
                    , veterinariaDto.DireccionLocal);

                var grupo = await _context.Grupos.Include(g => g.Permisos).FirstOrDefaultAsync(g => g.Id == 5); // el usuario se registra con id 2 que corresponde a Veterinaria
                if (grupo == null)
                {
                    throw new KeyNotFoundException("Grupo no encontrado.");
                }
                List<Grupo> grupos = new List<Grupo> { grupo };

                var usuarioExistente = await _context.Usuarios
                                            .Include(u => u.Persona)
                                                .ThenInclude(p => p.Localidad)
                                                    .ThenInclude(l => l.Provincia)
                                            .Include(u => u.Persona)
                                                .ThenInclude(p => p.Perfiles)
                                                    .ThenInclude(perfil => perfil.TipoPerfil)
                                            .Include(u => u.Grupos)
                                                .ThenInclude(g => g.Permisos)
                                            .FirstOrDefaultAsync(u => u.Id == userId); // seteo el perfil al usuario que se cargo como Veterinaria

                usuarioExistente.Persona.Perfiles.Add(newVeterinaria);


                usuarioExistente.Grupos.Add(grupo);


                if (usuarioExistente.Persona.Perfiles.Count == 4)
                {
                    usuarioExistente.Grupos.RemoveAll(g => g.Id == 2); // le quito el rol invitado si ya tiene los 4 perfiles cargados
                }
                await _context.SaveChangesAsync();

                return usuarioExistente;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
