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

                TipoPerfil tipoPerfilAdoptante = await _context.TiposPerfil.FirstOrDefaultAsync(p => p.Descripcion == "Adoptante");


                Adoptante newAdoptante = new Adoptante(tipoPerfilAdoptante, adoptanteDto.EstadoCivil, adoptanteDto.Ocupacion,
                    adoptanteDto.ExperienciaMascotas, adoptanteDto.NroMascotas, usuarioExistente.Persona);

                var grupo = await _context.Grupos.Include(g => g.Permisos).FirstOrDefaultAsync(g => g.Id == 3); // el usuario se registra con id 2 que corresponde a ADOPTANTE
                if (grupo == null)
                {
                    throw new KeyNotFoundException("Grupo no encontrado.");
                }

                List<Grupo> grupos = new List<Grupo> { grupo };



                // VALIDO SI YA TIENE UN PERFIL ACOPTANTE CARGADO para pisarlo si fue asi
                var perfilExistente = usuarioExistente.Persona.Perfiles
                                .OfType<Adoptante>()
                                .FirstOrDefault(); // Obtén el primer perfil de tipo Adoptante si existe

                if (perfilExistente != null)
                {
                    perfilExistente.UpdateAdoptante(newAdoptante.EstadoCivil, newAdoptante.Ocupacion, newAdoptante.ExperienciaMascotas, newAdoptante.NroMascotas);
                }
                else
                {
                    usuarioExistente.Persona.Perfiles.Add(newAdoptante);
                }



                usuarioExistente.Grupos.Add(grupo);

                //UpdateListOfHasToUpdateProfile ya que cargo ese permiso
                if (usuarioExistente.HasToUpdateProfile.Contains(1))
                {
                    // Eliminar el ID del permiso (1) de la lista
                    usuarioExistente.HasToUpdateProfile.Remove(1);

                    // Actualizar la lista después de modificarla
                    usuarioExistente.UpdateListOfHasToUpdateProfile(usuarioExistente.HasToUpdateProfile);
                }



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

                TipoPerfil tipoPerfilCliente = await _context.TiposPerfil.FirstOrDefaultAsync(p => p.Descripcion == "Cliente");

                Cliente newCliente = new Cliente(tipoPerfilCliente, clienteDto.Cuil, clienteDto.Ocupacion, usuarioExistente.Persona);

                var grupo = await _context.Grupos.Include(g => g.Permisos).FirstOrDefaultAsync(g => g.Id == 4); // el usuario se registra con id 2 que corresponde a CLIENTE
                if (grupo == null)
                {
                    throw new KeyNotFoundException("Grupo no encontrado.");
                }
                List<Grupo> grupos = new List<Grupo> { grupo };



                // VALIDO SI YA TIENE UN PERFIL CLIENTE CARGADO para pisarlo si fue asi
                var perfilExistente = usuarioExistente.Persona.Perfiles
                                .OfType<Cliente>()
                                .FirstOrDefault(); // Obtén el primer perfil de tipo Protectora si existe

                if (perfilExistente != null)
                {
                    perfilExistente.UpdateCliente(newCliente.Cuil, newCliente.Ocupacion);
                }
                else
                {
                    usuarioExistente.Persona.Perfiles.Add(newCliente);
                }


                usuarioExistente.Grupos.Add(grupo);

                //UpdateListOfHasToUpdateProfile ya que cargo ese permiso
                if (usuarioExistente.HasToUpdateProfile.Contains(2))
                {
                    // Eliminar el ID del permiso (1) de la lista
                    usuarioExistente.HasToUpdateProfile.Remove(2);

                    // Actualizar la lista después de modificarla
                    usuarioExistente.UpdateListOfHasToUpdateProfile(usuarioExistente.HasToUpdateProfile);
                }


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

                TipoPerfil tipoPerfilProtectora = await _context.TiposPerfil.FirstOrDefaultAsync(p => p.Descripcion == "Protectora");

                Protectora newProtectora = new Protectora(tipoPerfilProtectora, protectoraDto.Capacidad, protectoraDto.NroVoluntarios, new List<Pedido>(), new List<Producto>(), new List<Mascota>(), protectoraDto.CantidadInicialMascotas, usuarioExistente.Persona);

                var grupo = await _context.Grupos.Include(g => g.Permisos).FirstOrDefaultAsync(g => g.Id == 6); // el usuario se registra con id 2 que corresponde a Protectora
                if (grupo == null)
                {
                    throw new KeyNotFoundException("Grupo no encontrado.");
                }
                List<Grupo> grupos = new List<Grupo> { grupo };



                // VALIDO SI YA TIENE UN PERFIL Protectora CARGADO para pisarlo si fue asi
                var perfilExistente = usuarioExistente.Persona.Perfiles
                                .OfType<Protectora>()
                                .FirstOrDefault(); // Obtén el primer perfil de tipo Adoptante si existe

                if (perfilExistente != null)
                {
                    perfilExistente.UpdateProtectora(newProtectora.Capacidad, newProtectora.NroVoluntarios, newProtectora.Pedidos, newProtectora.Productos, newProtectora.Mascotas, newProtectora.CantidadInicialMascotas);
                }
                else
                {
                    usuarioExistente.Persona.Perfiles.Add(newProtectora);
                }


                usuarioExistente.Grupos.Add(grupo);

                //UpdateListOfHasToUpdateProfile ya que cargo ese permiso
                if (usuarioExistente.HasToUpdateProfile.Contains(4))
                {
                    // Eliminar el ID del permiso (1) de la lista
                    usuarioExistente.HasToUpdateProfile.Remove(4);

                    // Actualizar la lista después de modificarla
                    usuarioExistente.UpdateListOfHasToUpdateProfile(usuarioExistente.HasToUpdateProfile);
                }


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

                TipoPerfil tipoPerfilVeterinaria = await _context.TiposPerfil.FirstOrDefaultAsync(p => p.Descripcion == "Veterinaria");

                var tipoPlan = TipoPlan.Mensual;
                var fechaFin = DateTime.Today.AddDays(30);

                if (veterinariaDto.Suscripciones[0].Monto == 200)
                {
                    tipoPlan = TipoPlan.Anual;
                    fechaFin = DateTime.Today.AddYears(1);
                }


                // Crear instancia de Veterinaria sin suscripciones iniciales
                Veterinaria newVeterinaria = new Veterinaria(
                    tipoPerfilVeterinaria,
                    veterinariaDto.Latitud,
                    veterinariaDto.Longitud,
                    new List<Suscripcion>(), // Lista vacía inicial
                    veterinariaDto.DireccionLocal,
                    new List<Oferta>(),
                    veterinariaDto.Nombre,
                    veterinariaDto.Telefono, usuarioExistente.Persona
                );

                // Crear suscripciones asociadas a la veterinaria
                List<Suscripcion> suscripciones = new List<Suscripcion>();
                var suscripcionInicial = new Suscripcion(
                    veterinariaDto.Suscripciones[0].FechaInicio,
                    fechaFin,
                    veterinariaDto.Suscripciones[0].Monto,
                    true,
                    tipoPlan,
                    newVeterinaria
                );
                suscripciones.Add(suscripcionInicial);

                // Agregar las suscripciones a la veterinaria
                newVeterinaria.Suscripciones.AddRange(suscripciones);


                var grupo = await _context.Grupos.Include(g => g.Permisos).FirstOrDefaultAsync(g => g.Id == 5); // el usuario se registra con id 2 que corresponde a Veterinaria
                if (grupo == null)
                {
                    throw new KeyNotFoundException("Grupo no encontrado.");
                }
                List<Grupo> grupos = new List<Grupo> { grupo };



                // VALIDO SI YA TIENE UN PERFIL Veterinaria CARGADO para pisarlo si fue asi
                var perfilExistente = usuarioExistente.Persona.Perfiles
                                .OfType<Veterinaria>()
                                .FirstOrDefault(); // Obtén el primer perfil de tipo Veterinaria si existe

                if (perfilExistente != null)
                {
                    perfilExistente.UpdateVeterinaria(newVeterinaria.Latitud, newVeterinaria.Longitud, newVeterinaria.Suscripciones, newVeterinaria.DireccionLocal, newVeterinaria.Ofertas);
                }
                else
                {
                    usuarioExistente.Persona.Perfiles.Add(newVeterinaria);
                }



                usuarioExistente.Grupos.Add(grupo);

                //UpdateListOfHasToUpdateProfile ya que cargo ese permiso
                if (usuarioExistente.HasToUpdateProfile.Contains(3))
                {
                    // Eliminar el ID del permiso (1) de la lista
                    usuarioExistente.HasToUpdateProfile.Remove(3);

                    // Actualizar la lista después de modificarla
                    usuarioExistente.UpdateListOfHasToUpdateProfile(usuarioExistente.HasToUpdateProfile);
                }


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
