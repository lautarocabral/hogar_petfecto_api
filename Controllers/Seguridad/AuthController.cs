﻿using alumnos_api.Models;
using alumnos_api.Services.Interface;
using AutoMapper;
using Azure;
using hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Dtos;
using hogar_petfecto_api.Models.Dtos.Request;
using hogar_petfecto_api.Models.Dtos.Response;
using hogar_petfecto_api.Models.Perfiles;
using hogar_petfecto_api.Models.Seguridad;
using hogar_petfecto_api.Services;
using hogar_petfecto_api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace hogar_petfecto_api.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly GestionDbContext _context;

        public AuthController(IUnitOfWork unitOfWork, IMapper mapper, GestionDbContext context)
        {
            //_authService = authService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var usuario = await _unitOfWork.AuthService.ValidarCredencialesAsync(request.Email, request.Password);
            if (usuario == null)
            {
                //return Unauthorized(ApiResponse<string>.Error("Credenciales inválidas", 401));
                return Ok(ApiResponse<LoginResponseDto>.UnAuthorizedToken("Credenciales inválidas"));
            }

            var token = _unitOfWork.AuthService.GenerarToken(usuario);


            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            var response = new LoginResponseDto
            {
                token = token,
                UsuarioResponseDto = usuarioDto
            };

            return Ok(ApiResponse<LoginResponseDto>.Success(response));
        }





        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDto signUpDtoRequest)
        {
            var newUserResponse = await _unitOfWork.AuthService.SignUp(signUpDtoRequest);

            if (newUserResponse.StatusCode < 200 || newUserResponse.StatusCode >= 300)
            {
                //return BadRequest(new { message =  });
                return Ok(ApiResponse<string>.Error(newUserResponse.Message));
            }

            var token = _unitOfWork.AuthService.GenerarToken(newUserResponse.Result);

            //return Ok(new { token });
            return Ok(ApiResponse<string>.Success("Usuario registrado con exito"));
        }


        [HttpGet("GetPermisos")]
        public async Task<IActionResult> GetPermisos()
        {
            // AUTH/////////////////////////////////////////////////////////////////////////////////
            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);
            if (claimsPrincipal == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));
            }
            var userId = claimsPrincipal.FindFirst("userId")?.Value;
            var usuario = await _unitOfWork.AuthService.ReturnUsuario(userId);
            var token = _unitOfWork.AuthService.GenerarToken(usuario);
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 9));

            if (!hasPermiso)
            {
                return Unauthorized(ApiResponse<string>.Error("No tiene permisos para obtener permisos", 401));
            }
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            //AUTH/////////////////////////////////////////////////////////////////////////////////


            var resultado = await _context.Permisos.ToListAsync();

            var permisoDtos = _mapper.Map<List<PermisoDto>>(resultado);

            var response = new PermisosResponseDto
            {
                token = token,
                PermisosDto = permisoDtos
            };

            return Ok(ApiResponse<PermisosResponseDto>.Success(response));
        }

        [HttpGet("GetGrupos")]
        public async Task<IActionResult> GetGrupos()
        {
            // AUTH/////////////////////////////////////////////////////////////////////////////////
            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);
            if (claimsPrincipal == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));
            }
            var userId = claimsPrincipal.FindFirst("userId")?.Value;
            var usuario = await _unitOfWork.AuthService.ReturnUsuario(userId);
            var token = _unitOfWork.AuthService.GenerarToken(usuario);
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 9));

            if (!hasPermiso)
            {
                return Unauthorized(ApiResponse<string>.Error("No tiene permisos para obtener grupos", 401));
            }
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            //AUTH/////////////////////////////////////////////////////////////////////////////////

            try
            {
                var resultado = await _context.Grupos.Include(p => p.Permisos).ToListAsync();

                var gruposDto = _mapper.Map<List<GrupoDto>>(resultado);

                var response = new GruposResponseDto
                {
                    token = token,
                    GruposDto = gruposDto
                };

                return Ok(ApiResponse<GruposResponseDto>.Success(response));
            }
            catch (Exception e)
            {

                return Ok(ApiResponse<string>.Error(e.Message));
            }
        }

        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            // AUTH/////////////////////////////////////////////////////////////////////////////////
            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);
            if (claimsPrincipal == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));
            }
            var userId = claimsPrincipal.FindFirst("userId")?.Value;
            var usuario = await _unitOfWork.AuthService.ReturnUsuario(userId);
            var token = _unitOfWork.AuthService.GenerarToken(usuario);
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 9));

            if (!hasPermiso)
            {
                return Unauthorized(ApiResponse<string>.Error("No tiene permisos para obtener usuarios", 401));
            }
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            //AUTH/////////////////////////////////////////////////////////////////////////////////

            try
            {
                var resultado = await _context.Usuarios
               .Include(p => p.Persona).ThenInclude(per => per.Perfiles).ThenInclude(per => per.TipoPerfil).Include(p => p.Persona).ThenInclude(l => l.Localidad).ThenInclude(l => l.Provincia)
               .Include(g => g.Grupos).ThenInclude(m => m.Permisos).ToListAsync();

                var usuariosDto = _mapper.Map<List<UsuarioDto>>(resultado);

                var response = new ListUsuariosResponseDto
                {
                    token = token,
                    UsuarioDtos = usuariosDto
                };

                return Ok(ApiResponse<ListUsuariosResponseDto>.Success(response));
            }
            catch (Exception e)
            {
                return Ok(ApiResponse<string>.Error(e.Message));
            }
        }

        [HttpPost("EditarUsuario")]
        public async Task<IActionResult> EditarUsuario([FromBody] EditarUsuarioRequestDto editarUsuarioRequestDto)
        {
            // Autenticación
            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);
            if (claimsPrincipal == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));
            }

            var userId = claimsPrincipal.FindFirst("userId")?.Value;
            var usuario = await _unitOfWork.AuthService.ReturnUsuario(userId);
            var token = _unitOfWork.AuthService.GenerarToken(usuario);

            // Validación de permisos
            bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 9));
            if (!hasPermiso)
            {
                return Unauthorized(ApiResponse<string>.Error("No tiene permisos para obtener permisos", 401));
            }

            var usuarioAModificar = await _context.Usuarios
                         .Include(u => u.Persona)
                             .ThenInclude(p => p.Localidad)
                                 .ThenInclude(l => l.Provincia)
                         .Include(u => u.Persona)
                             .ThenInclude(p => p.Perfiles)
                                 .ThenInclude(perfil => perfil.TipoPerfil)
                         .Include(u => u.Grupos)
                             .ThenInclude(g => g.Permisos)
                         .FirstOrDefaultAsync(u => u.PersonaDni == editarUsuarioRequestDto.Dni);

            if (usuarioAModificar == null)
            {
                return NotFound(ApiResponse<string>.Error("Usuario no encontrado", 404));
            }

            // Actualizar datos del usuario
            usuarioAModificar.UpdateUser(editarUsuarioRequestDto.Email, HashPassword(editarUsuarioRequestDto.Password));
            usuarioAModificar.Persona.UpdatePersona(editarUsuarioRequestDto.RazonSocial);

            // Gestión de grupos
            var gruposActualesIds = usuarioAModificar.Grupos.Select(g => g.Id).ToList();
            var nuevosGruposIds = editarUsuarioRequestDto.NewRoles;

            // Identificar grupos agregados
            var gruposAgregadosIds = nuevosGruposIds.Except(gruposActualesIds).ToList();

            // Obtener los permisos actuales basados en los nuevos grupos seleccionados
            var permisosActualesIds = await _context.Grupos
                .Where(g => nuevosGruposIds.Contains(g.Id))
                .SelectMany(g => g.Permisos)
                .Select(p => p.Id)
                .Where(id => new[] { 1, 2, 3, 4 }.Contains(id)) // Solo consideramos permisos 1, 2, 3, 4
                .Distinct()
                .ToListAsync();

            // Obtener los permisos actuales del usuario basados en los grupos actuales
            var permisosUsuarioActualesIds = usuarioAModificar.Grupos
                .SelectMany(g => g.Permisos)
                .Select(p => p.Id)
                .Where(id => new[] { 1, 2, 3, 4 }.Contains(id))
                .Distinct()
                .ToHashSet(); // HashSet para optimizar búsquedas

            // Obtener los nuevos permisos que no están ya en los permisos actuales del usuario
            var nuevosPermisosParaActualizar = permisosActualesIds
                .Where(id => !permisosUsuarioActualesIds.Contains(id)) // Filtrar permisos que no tenía previamente
                .ToList();

            // Eliminar perfiles que ya no tienen permisos correspondientes
            var tipoPerfilPermisosMap = new Dictionary<int, int>
                {
                    { 1, 1 }, // Adoptante -> Permiso 1
                    { 2, 2 }, // Cliente -> Permiso 2
                    { 3, 3 }, // Veterinaria -> Permiso 3
                    { 4, 4 }  // Protectora -> Permiso 4
                };

            var perfilesAEliminar = usuarioAModificar.Persona.Perfiles
                .Where(perfil =>
                    tipoPerfilPermisosMap.TryGetValue(perfil.TipoPerfil.Id, out var permisoId) &&
                    !permisosActualesIds.Contains(permisoId)) // Eliminar si no corresponde al nuevo permiso
                .ToList();

            foreach (var perfil in perfilesAEliminar)
            {
                // Verificar dependencias dinámicamente
                if (await TieneDependenciasAsync(perfil))
                {
                    return BadRequest(ApiResponse<string>.Error($"No se puede eliminar el perfil {perfil.GetType().Name} porque tiene dependencias asociadas."));
                }

                // Eliminar el perfil
                usuarioAModificar.Persona.Perfiles.Remove(perfil);
                _context.Remove(perfil);
            }

            // Actualizar la lista HasToUpdateProfile con los nuevos permisos
            usuarioAModificar.UpdateListOfHasToUpdateProfile(nuevosPermisosParaActualizar);

            // Actualizar la lista de grupos del usuario
            var listaDeRolesNuevos = await _context.Grupos
                .Where(g => nuevosGruposIds.Contains(g.Id))
                .ToListAsync();

            usuarioAModificar.UpdateGrupos(listaDeRolesNuevos);

            // Guardar cambios en la base de datos
            await _context.SaveChangesAsync();



            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            var response = new LoginResponseDto
            {
                token = token,
                UsuarioResponseDto = usuarioDto
            };

            await _context.SaveChangesAsync();

            return Ok(ApiResponse<LoginResponseDto>.Success(response));
        }

        private async Task<bool> TieneDependenciasAsync(Perfil perfil)
        {
            if (perfil is Veterinaria veterinaria)
            {
                return await _context.Suscripciones.AnyAsync(s => s.VeterinariaId == veterinaria.Id);
            }
            else if (perfil is Protectora protectora)
            {
                //var tienePedidos = await _context.Pedidos.AnyAsync(p => p.Protectora.Id == protectora.Id);
                var tieneProductos = await _context.Productos.AnyAsync(p => p.ProtectoraId == protectora.Id);
                var tieneMascotas = await _context.Mascotas.AnyAsync(m => m.ProtectoraId == protectora.Id);

                return tieneProductos || tieneMascotas;
            }

            return false;
        }

        [HttpGet("DeleteUsuario/{dni}")]
        public async Task<IActionResult> DeleteUsuario(string dni)
        {
            // AUTH/////////////////////////////////////////////////////////////////////////////////
            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);
            if (claimsPrincipal == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));
            }
            var userId = claimsPrincipal.FindFirst("userId")?.Value;
            var usuario = await _unitOfWork.AuthService.ReturnUsuario(userId);
            var token = _unitOfWork.AuthService.GenerarToken(usuario);

            bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 9));
            if (!hasPermiso)
            {
                return Unauthorized(ApiResponse<string>.Error("No tiene permisos para obtener usuarios", 401));
            }

            try
            {
                var usuarioAEliminar = await _context.Usuarios
                    .Include(p => p.Persona).ThenInclude(per => per.Perfiles).ThenInclude(per => per.TipoPerfil)
                    .Include(p => p.Persona).ThenInclude(l => l.Localidad).ThenInclude(l => l.Provincia)
                    .Include(g => g.Grupos).ThenInclude(m => m.Permisos)
                    .FirstOrDefaultAsync(user => user.PersonaDni == dni);

                if (usuarioAEliminar == null)
                {
                    return NotFound(ApiResponse<string>.Error("Usuario no encontrado", 404));
                }

                // Manejar relaciones con mascotas
                if (usuarioAEliminar.Persona.Perfiles.OfType<Protectora>().Any())
                {
                    var protectoraPerfil = usuarioAEliminar.Persona.Perfiles.OfType<Protectora>().First();

                    var mascotasAsociadas = await _context.Mascotas
                        .Where(m => m.ProtectoraId == protectoraPerfil.Id)
                        .ToListAsync();

                    // Eliminar mascotas asociadas
                    _context.Mascotas.RemoveRange(mascotasAsociadas);
                }

                // Eliminar el usuario
                _context.Usuarios.Remove(usuarioAEliminar);
                await _context.SaveChangesAsync();

                var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

                var response = new LoginResponseDto
                {
                    token = token,
                    UsuarioResponseDto = usuarioDto
                };

                return Ok(ApiResponse<LoginResponseDto>.Success(response));
            }
            catch (Exception e)
            {
                return Ok(ApiResponse<string>.Error(e.Message));
            }
        }


        [HttpPost("EditarGrupo")]
        public async Task<IActionResult> EditarGrupo(EditarGrupoResponseDto editarGrupoResponseDto)
        {
            // AUTH/////////////////////////////////////////////////////////////////////////////////
            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);
            if (claimsPrincipal == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));
            }
            var userId = claimsPrincipal.FindFirst("userId")?.Value;
            var usuario = await _unitOfWork.AuthService.ReturnUsuario(userId);
            var token = _unitOfWork.AuthService.GenerarToken(usuario);
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 9));

            if (!hasPermiso)
            {
                return Unauthorized(ApiResponse<string>.Error("No tiene permisos para obtener permisos", 401));
            }
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            //AUTH/////////////////////////////////////////////////////////////////////////////////

            // Obtener grupo
            var grupo = await _context.Grupos
                .Include(g => g.Permisos) // Incluimos los permisos existentes para actualización
                .FirstOrDefaultAsync(g => g.Id == editarGrupoResponseDto.GrupoId);

            if (grupo == null)
            {
                return NotFound(ApiResponse<string>.Error($"Grupo con ID {editarGrupoResponseDto.GrupoId} no encontrado", 404));
            }

            // Validar permisos proporcionados
            var permisos = await _context.Permisos
                .Where(p => editarGrupoResponseDto.PermisosId.Contains(p.Id))
                .ToListAsync();

            if (permisos.Count != editarGrupoResponseDto.PermisosId.Count)
            {
                return BadRequest(ApiResponse<string>.Error("Algunos permisos no existen en el sistema", 400));
            }

            // Actualizar permisos del grupo
            grupo.UpdatePermisos(permisos);

            // Guardar cambios en la base de datos
            await _context.SaveChangesAsync();

            // Generar respuesta
            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            var response = new LoginResponseDto
            {
                token = token,
                UsuarioResponseDto = usuarioDto
            };

            return Ok(ApiResponse<LoginResponseDto>.Success(response));
        }


        [HttpPost("AgregarGrupo")]
        public async Task<IActionResult> AgregarGrupo(EditarGrupoResponseDto editarGrupoResponseDto)
        {
            // AUTH/////////////////////////////////////////////////////////////////////////////////
            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);
            if (claimsPrincipal == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));
            }
            var userId = claimsPrincipal.FindFirst("userId")?.Value;
            var usuario = await _unitOfWork.AuthService.ReturnUsuario(userId);
            var token = _unitOfWork.AuthService.GenerarToken(usuario);
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 9));

            if (!hasPermiso)
            {
                return Unauthorized(ApiResponse<string>.Error("No tiene permisos para obtener permisos", 401));
            }
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            //AUTH/////////////////////////////////////////////////////////////////////////////////

            // Validar permisos proporcionados
            var permisos = await _context.Permisos
                .Where(p => editarGrupoResponseDto.PermisosId.Contains(p.Id))
                .ToListAsync();

            if (permisos.Count != editarGrupoResponseDto.PermisosId.Count)
            {
                return BadRequest(ApiResponse<string>.Error("Algunos permisos no existen en el sistema", 400));
            }

            var newGrupo = new Grupo(editarGrupoResponseDto.GrupoNombre, permisos);

            // Obtener grupo
            _context.Grupos.Add(newGrupo);


            // Guardar cambios en la base de datos
            await _context.SaveChangesAsync();

            // Generar respuesta
            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            var response = new LoginResponseDto
            {
                token = token,
                UsuarioResponseDto = usuarioDto
            };

            return Ok(ApiResponse<LoginResponseDto>.Success(response));
        }

        [HttpGet("DeleteGrupo/{id}")]
        public async Task<IActionResult> DeleteGrupo(int id)
        {
            // AUTH/////////////////////////////////////////////////////////////////////////////////
            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);
            if (claimsPrincipal == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));
            }
            var userId = claimsPrincipal.FindFirst("userId")?.Value;
            var usuario = await _unitOfWork.AuthService.ReturnUsuario(userId);
            var token = _unitOfWork.AuthService.GenerarToken(usuario);
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 9));

            if (!hasPermiso)
            {
                return Unauthorized(ApiResponse<string>.Error("No tiene permisos para obtener permisos", 401));
            }
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            //AUTH/////////////////////////////////////////////////////////////////////////////////

            var grupo = await _context.Grupos.FirstOrDefaultAsync(g => g.Id == id);

            if (grupo == null)
            {
                return NotFound(ApiResponse<string>.Error($"Grupo con ID {id} no encontrado", 404));
            }

            // Eliminar el grupo
            _context.Grupos.Remove(grupo);

            // Guardar cambios en la base de datos
            await _context.SaveChangesAsync();

            // Generar respuesta
            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            var response = new LoginResponseDto
            {
                token = token,
                UsuarioResponseDto = usuarioDto
            };

            return Ok(ApiResponse<LoginResponseDto>.Success(response));
        }
    }

}
