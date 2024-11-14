// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using alumnos_api.Models;
using alumnos_api.Services.Interface;
using AutoMapper;
using hogar_petfecto_api.Models.Dtos.Response;
using hogar_petfecto_api.Models.Dtos;
using hogar_petfecto_api.Models.Perfiles;
using hogar_petfecto_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

namespace hogar_petfecto_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdopcionesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly GestionDbContext _context;

        public AdopcionesController(IUnitOfWork unitOfWork, IMapper mapper, GestionDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("CargaPostulacion/{mascotaId}")]
        public async Task<IActionResult> CargaPostulacion(int mascotaId)
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
            bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 1));

            if (!hasPermiso)
            {
                return Unauthorized(ApiResponse<string>.Error("No tiene permisos para cargar una adopcion", 401));
            } 
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            //AUTH/////////////////////////////////////////////////////////////////////////////////



            var usuarioAdoptante = await _context.Usuarios
                .Include(u => u.Persona)
                    .ThenInclude(p => p.Perfiles)
                .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));

            if (usuarioAdoptante == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            // Filtrar el perfil para obtener el perfil de tipo Adoptante
            var adoptantePerfil = usuario.Persona.Perfiles
                .OfType<Adoptante>()
                .FirstOrDefault();

            //VALIDO SI YA HIZO LA POSTULACION PARA ESTA MASCOTA//////////////
            bool exists = await _context.Postulaciones
                             .Include(p => p.Adoptante)
                             .Include(p => p.Mascota)
                             .AnyAsync(p => p.Adoptante.Id == adoptantePerfil.Id && p.Mascota.Id == mascotaId);

            if (exists)
            {
                return Ok(ApiResponse<string>.Error("Ya te postulaste para adoptar esta mascota"));
            }
            //VALIDO SI YA HIZO LA POSTULACION PARA ESTA MASCOTA//////////////

            ////////////////// VALIDO SI QUIERE AUTOADOPTARSE MASCOTA//////////////////////////////

            var usuarioProtectora = await _context.Usuarios
                                              .Include(u => u.Persona)
                                              .ThenInclude(p => p.Perfiles)
                                              .ThenInclude(perfil => (perfil as Protectora).Mascotas) // Safely cast in ThenInclude
                                              .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));

            bool existeMascota = usuarioProtectora?.Persona.Perfiles
                .OfType<Protectora>()
                .Any(protectora => protectora.Mascotas.Any(mascota => mascota.Id == mascotaId)) ?? false;

            if (existeMascota)
            {
                return Ok(ApiResponse<string>.Error("No podes postularte para adoptar una mascota propia"));
            }
            ////////////////// VALIDO SI QUIERE AUTOADOPTARSE MASCOTA//////////////////////////////


            if (adoptantePerfil == null)
            {
                throw new Exception("El usuario no tiene un perfil de tipo Adoptante");
            }

            // Obtener la mascota y el estado de la postulación
            Mascota mascota = await _context.Mascotas.FirstOrDefaultAsync(m => m.Id == mascotaId);
            if (mascota == null)
            {
                throw new Exception("Mascota no encontrada");
            }

            EstadoPostulacion estadoPostulacion = await _context.EstadosPostulacion.FirstOrDefaultAsync(e => e.Id == 2);
            if (estadoPostulacion == null)
            {
                throw new Exception("Estado de postulación no encontrado");
            }

            Postulacion newPostulacion = new Postulacion(adoptantePerfil, mascota, DateTime.Today, estadoPostulacion);

            _context.Postulaciones.Add(newPostulacion);

            await _context.SaveChangesAsync();
            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
            var response = new LoginResponseDto
            {
                token = token,
                UsuarioResponseDto = usuarioDto
            };



            return Ok(ApiResponse<LoginResponseDto>.Success(response));
        }

        [HttpGet("GetMisPostulaciones/{adoptanteOProtectoraId}")]
        public async Task<IActionResult> GetMisPostulaciones(int adoptanteOProtectoraId)
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
            bool hasPermiso = usuario.Grupos.Any(grupo =>
                                        grupo.Permisos.Any(p => p.Id == 1 || p.Id == 4)
                                    );


            if (!hasPermiso)
            {
                return Unauthorized(ApiResponse<string>.Error("No tiene permisos para obtener postulaciones", 401));
            }
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            //AUTH/////////////////////////////////////////////////////////////////////////////////


            var postulaciones = await _context.Postulaciones

                .Include(a => a.Adoptante).Include(a => a.Mascota).Include(a => a.Estado).
                Where(p => p.Adoptante.Id == adoptanteOProtectoraId).ToListAsync();

            var postulacionesDto = _mapper.Map<List<PostulacionDto>>(postulaciones);
            var response = new PostulacionesResponseDto
            {
                token = token,
                PostulacionDtos = postulacionesDto
            };


            return Ok(ApiResponse<PostulacionesResponseDto>.Success(response));
        }

        [HttpGet("DeletePostulacion/{postulacionId}")]
        public async Task<IActionResult> DeletePostulacion(int postulacionId)
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
            bool hasPermiso = usuario.Grupos.Any(grupo =>
                                        grupo.Permisos.Any(p => p.Id == 1 || p.Id == 4)
                                    );
            if (!hasPermiso)
            {
                Ok(ApiResponse<string>.Error("No tiene permisos para eliminar postulaciones"));
            }
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            //AUTH/////////////////////////////////////////////////////////////////////////////////


            var postulacion = await _context.Postulaciones.Include(a => a.Adoptante).Include(a => a.Mascota).Include(a => a.Estado).FirstOrDefaultAsync(p => p.Id == postulacionId);

            _context.Postulaciones.Remove(postulacion);

            await _context.SaveChangesAsync();

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
