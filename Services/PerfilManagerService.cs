using alumnos_api.Models;
using hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Perfiles;
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
        public async Task<ApiResponse<Adoptante>> CargarAdoptante(Adoptante adoptante)
        {
            if (adoptante == null)
            {
                return ApiResponse<Adoptante>.Error("El objeto Adoptante no puede ser null.");
            }

            if (adoptante.FechaNacimiento == default(DateTime) || string.IsNullOrEmpty(adoptante.Ocupacion))
            {
                return ApiResponse<Adoptante>.Error("Los campos Fecha de Nacimiento y Ocupación son obligatorios.");
            }

            var existingAdoptante = await _context.Adoptantes
                .FirstOrDefaultAsync(a => a.FechaNacimiento == adoptante.FechaNacimiento && a.Ocupacion == adoptante.Ocupacion);

            if (existingAdoptante != null)
            {
                return ApiResponse<Adoptante>.Error("Ya existe un adoptante con los mismos datos.");
            }

            try
            {
                TipoPerfil tipoPerfilAdoptante = await _context.TiposPerfil.FirstOrDefaultAsync(p => p.Descripcion == "Adoptante");

                if (tipoPerfilAdoptante == null)
                {
                    return ApiResponse<Adoptante>.Error("No se encontró el tipo de perfil 'Adoptante' en la base de datos.");
                }

                Adoptante newAdoptante = new Adoptante(tipoPerfilAdoptante, adoptante.FechaNacimiento, adoptante.EstadoCivil, adoptante.Ocupacion,
                    adoptante.ExperienciaMascotas, adoptante.NroMascotas);

                _context.Adoptantes.Add(newAdoptante);
                await _context.SaveChangesAsync();

                return ApiResponse<Adoptante>.Success(newAdoptante, "Adoptante cargado exitosamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<Adoptante>.Error($"Ocurrió un error al cargar el adoptante: {ex.Message}");
            }
        }




        public async Task<ApiResponse<Cliente>> CargarCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                return ApiResponse<Cliente>.Error("El objeto Cliente no puede ser null.");
            }

            if (string.IsNullOrEmpty(cliente.Cuil) || string.IsNullOrEmpty(cliente.Ocupacion))
            {
                return ApiResponse<Cliente>.Error("Los campos CUIL y Ocupación son obligatorios.");
            }

            // Validación: Verifica si ya existe un Cliente con el mismo CUIL
            var existingCliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Cuil == cliente.Cuil);

            if (existingCliente != null)
            {
                return ApiResponse<Cliente>.Error("Ya existe un cliente con el mismo CUIL.");
            }

            try
            {
                // Busca el tipo de perfil 'Cliente'
                TipoPerfil tipoPerfilCliente = await _context.TiposPerfil.FirstOrDefaultAsync(p => p.Descripcion == "Cliente");

                if (tipoPerfilCliente == null)
                {
                    return ApiResponse<Cliente>.Error("No se encontró el tipo de perfil 'Cliente' en la base de datos.");
                }

                // Crea un nuevo objeto Cliente con el perfil adecuado
                Cliente newCliente = new Cliente(tipoPerfilCliente, cliente.Cuil, cliente.Ocupacion);

                _context.Clientes.Add(newCliente);
                await _context.SaveChangesAsync();

                // Retorna el cliente recién creado
                return ApiResponse<Cliente>.Success(newCliente, "Cliente cargado exitosamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<Cliente>.Error($"Ocurrió un error al cargar el cliente: {ex.Message}");
            }
        }




        public async Task<ApiResponse<Protectora>> CargarProtectora(Protectora protectora)
        {
            if (protectora == null)
            {
                return ApiResponse<Protectora>.Error("El objeto Protectora no puede ser null.");
            }

            if (protectora.Capacidad <= 0 || protectora.NroVoluntarios < 0)
            {
                return ApiResponse<Protectora>.Error("Los campos Capacidad y Número de Voluntarios son obligatorios y deben ser válidos.");
            }

            // Validación: Verifica si ya existe una protectora con el mismo Id (o cualquier otra clave única que utilices)
            var existingProtectora = await _context.Protectoras
                .FirstOrDefaultAsync(p => p.Id == protectora.Id);

            if (existingProtectora != null)
            {
                return ApiResponse<Protectora>.Error("Ya existe una protectora con el mismo ID.");
            }

            try
            {
                // Busca el tipo de perfil 'Protectora'
                TipoPerfil tipoPerfilProtectora = await _context.TiposPerfil.FirstOrDefaultAsync(p => p.Descripcion == "Protectora");

                if (tipoPerfilProtectora == null)
                {
                    return ApiResponse<Protectora>.Error("No se encontró el tipo de perfil 'Protectora' en la base de datos.");
                }

                // Crea una nueva protectora
                Protectora newProtectora = new Protectora(tipoPerfilProtectora, protectora.Capacidad, protectora.NroVoluntarios, protectora.Pedidos, protectora.Productos, protectora.Mascotas);

                _context.Protectoras.Add(newProtectora);
                await _context.SaveChangesAsync();

                // Retorna la protectora recién creada
                return ApiResponse<Protectora>.Success(newProtectora, "Protectora cargada exitosamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<Protectora>.Error($"Ocurrió un error al cargar la protectora: {ex.Message}");
            }
        }



        public async Task<ApiResponse<Veterinaria>> CargarVeterinaria(Veterinaria veterinaria)
        {
            if (veterinaria == null || string.IsNullOrEmpty(veterinaria.DireccionLocal))
            {
                return ApiResponse<Veterinaria>.Error("El objeto Veterinaria y la dirección no pueden ser null.");
            }

            // Obtener la latitud y longitud usando el servicio de geocodificación
            try
            {
                var geocodingService = new GeocodingService();
                var (latitud, longitud) = await geocodingService.GetLatLongFromAddressAsync(veterinaria.DireccionLocal);

                // Validar si ya existe una veterinaria en la misma ubicación
                var existingVeterinaria = await _context.Veterinarias
                    .FirstOrDefaultAsync(v => v.Latitud == latitud && v.Longitud == longitud);

                if (existingVeterinaria != null)
                {
                    return ApiResponse<Veterinaria>.Error("Ya existe una veterinaria con la misma ubicación.");
                }

                // Buscar el tipo de perfil 'Veterinaria'
                TipoPerfil tipoPerfilVeterinaria = await _context.TiposPerfil.FirstOrDefaultAsync(p => p.Descripcion == "Veterinaria");

                if (tipoPerfilVeterinaria == null)
                {
                    return ApiResponse<Veterinaria>.Error("No se encontró el tipo de perfil 'Veterinaria'.");
                }

                // Crear una nueva veterinaria con latitud y longitud obtenidas
                Veterinaria newVeterinaria = new Veterinaria(tipoPerfilVeterinaria, latitud, longitud, veterinaria.Suscripciones, veterinaria.DireccionLocal);

                _context.Veterinarias.Add(newVeterinaria);
                await _context.SaveChangesAsync();

                return ApiResponse<Veterinaria>.Success(newVeterinaria, "Veterinaria cargada exitosamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<Veterinaria>.Error($"Ocurrió un error al cargar la veterinaria: {ex.Message}");
            }
        }


    }
}
