// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using alumnos_api.Services.Interface;
using AutoMapper;
using hogar_petfecto_api.Models.Dtos.Response;
using hogar_petfecto_api.Models.Dtos;
using hogar_petfecto_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using alumnos_api.Models;
using hogar_petfecto_api.Models.Dtos.Request;
using hogar_petfecto_api.Models.Perfiles;

namespace hogar_petfecto_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly GestionDbContext _context;

        public MascotasController(IUnitOfWork unitOfWork, IMapper mapper, GestionDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("GetMascotasForProtectora")]
        public async Task<IActionResult> GetMascotasForProtectora()
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
            bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 4));

            if (!hasPermiso)
            {
                return Unauthorized(ApiResponse<string>.Error("No tiene permisos para obtener las mascotas", 401));
            }
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            //AUTH/////////////////////////////////////////////////////////////////////////////////


            var usuarioProtectora = await _context.Usuarios
                       .Include(u => u.Persona)
                           .ThenInclude(p => p.Perfiles) // Include all Perfiles without filtering
                       .ThenInclude(p => ((Protectora)p).Mascotas).ThenInclude(t => t.TipoMascota)
                       .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));



            var protectoraProfile = usuarioProtectora.Persona.Perfiles
                    .OfType<Protectora>()
                    .FirstOrDefault();

            var mascotas = protectoraProfile.Mascotas;

            var mascotaDto = _mapper.Map<List<MascotaDto>>(mascotas);

            var response = new MascotasResponseDto
            {
                token = token,
                MascotasDto = mascotaDto
            };



            return Ok(ApiResponse<MascotasResponseDto>.Success(response));
        }

        [HttpPost("CargaMascota")]
        public async Task<IActionResult> CargaMascota([FromBody] MascotaRequestDto mascota)
        {

            //AUTH/////////////////////////////////////////////////////////////////////////////////
            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);
            if (claimsPrincipal == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));
            }
            var userId = claimsPrincipal.FindFirst("userId")?.Value;
            var usuario = await _unitOfWork.AuthService.ReturnUsuario(userId);
            var token = _unitOfWork.AuthService.GenerarToken(usuario);
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 4));

            if (!hasPermiso)
            {
                return Unauthorized(ApiResponse<string>.Error("No tiene permisos para dar de alta una mascota", 401));
            }
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            //AUTH/////////////////////////////////////////////////////////////////////////////////

            // Retrieve the TipoMascota entity
            var tipoMascota = await _context.TiposMascota.FirstOrDefaultAsync(t => t.Id == mascota.TipoMascota);
            var usuarioProtectora = await _context.Usuarios
                .Include(u => u.Persona)
                    .ThenInclude(p => p.Perfiles)
                .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));

            var protectoraProfile = usuarioProtectora.Persona.Perfiles
                .OfType<Protectora>()
                .FirstOrDefault(per => per.TipoPerfil != null && per.TipoPerfil.Id == 4);

            // Add the new Mascota to the Protectora's Mascotas list
            protectoraProfile.AddMascota(new Mascota(
                              tipoMascota,
                              mascota.Nombre,
                              mascota.Peso,
                              mascota.AptoDepto,
                              mascota.AptoPerros,
                              mascota.FechaNacimiento,
                              mascota.Castrado,
                              mascota.Sexo,
                              mascota.Vacunado,
                              false,
                              mascota.Imagen));

            await _context.SaveChangesAsync();

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            var response = new LoginResponseDto
            {
                token = token,
                UsuarioResponseDto = usuarioDto
            };

            return Ok(ApiResponse<LoginResponseDto>.Success(response));
        }

        [HttpGet("GetTipoMascotas")]
        public async Task<IActionResult> GetTipoMascotas()
        {
            //AUTH/////////////////////////////////////////////////////////////////////////////////
            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);
            if (claimsPrincipal == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));
            }
            var userId = claimsPrincipal.FindFirst("userId")?.Value;
            var usuario = await _unitOfWork.AuthService.ReturnUsuario(userId);
            var token = _unitOfWork.AuthService.GenerarToken(usuario);
            //AUTH/////////////////////////////////////////////////////////////////////////////////

            var tipoMascotas = await _context.TiposMascota.ToListAsync();

            var response = new TipoMascotaResponseDto
            {
                token = token,
                TiposMascotas = tipoMascotas
            };

            return Ok(ApiResponse<TipoMascotaResponseDto>.Success(response));
        }

        [HttpGet("DeleteMascota/{id}")]
        public async Task<IActionResult> DeleteMascota(int id)
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
            bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 4));
            if (!hasPermiso)
            {
                return Unauthorized(ApiResponse<string>.Error("No tiene permisos para editar la mascota", 401));
            }
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            //AUTH/////////////////////////////////////////////////////////////////////////////////

            // Retrieve the Protectora profile with related Mascotas
            var usuarioProtectora = await _context.Usuarios
                .Include(u => u.Persona)
                    .ThenInclude(p => p.Perfiles)
                .ThenInclude(p => ((Protectora)p).Mascotas)
                .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));

            if (usuarioProtectora == null || usuarioProtectora.Persona == null)
            {
                return NotFound("Usuario o persona no encontrado.");
            }

            var protectoraProfile = usuarioProtectora.Persona.Perfiles
                .OfType<Protectora>()
                .FirstOrDefault();

            if (protectoraProfile == null)
            {
                return NotFound("Perfil de tipo Protectora no encontrado.");
            }

            // Find the Mascota with the given ID
            var mascotaToDelete = protectoraProfile.Mascotas.FirstOrDefault(m => m.Id == id);
            if (mascotaToDelete == null)
            {
                return NotFound($"Mascota con ID {id} no encontrada.");
            }

            // Remove the Mascota and save changes
            protectoraProfile.Mascotas.Remove(mascotaToDelete);
            await _context.SaveChangesAsync();

            var mascotaDto = _mapper.Map<List<MascotaDto>>(protectoraProfile.Mascotas);

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
            var response = new LoginResponseDto
            {
                token = token,
                UsuarioResponseDto = usuarioDto
            };

            return Ok(ApiResponse<LoginResponseDto>.Success(response));
        }

        [HttpPost("EditarMascota")]
        public async Task<IActionResult> EditarMascota([FromBody] MascotaDto mascota)
        {

            //AUTH/////////////////////////////////////////////////////////////////////////////////
            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);
            if (claimsPrincipal == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));
            }
            var userId = claimsPrincipal.FindFirst("userId")?.Value;
            var usuario = await _unitOfWork.AuthService.ReturnUsuario(userId);
            var token = _unitOfWork.AuthService.GenerarToken(usuario);
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 4));

            if (!hasPermiso)
            {
                return Unauthorized(ApiResponse<string>.Error("No tiene permisos para dar de alta una mascota", 401));
            }
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            //AUTH/////////////////////////////////////////////////////////////////////////////////

            // Retrieve the TipoMascota entity
            var tipoMascota = await _context.TiposMascota.FirstOrDefaultAsync(t => t.Id == mascota.TipoMascota.Id);

            var usuarioProtectora = await _context.Usuarios
     .Include(u => u.Persona)
         .ThenInclude(p => p.Perfiles)
     .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));

            if (usuarioProtectora == null || usuarioProtectora.Persona == null)
            {
                return NotFound("Usuario o persona no encontrado.");
            }

            // Find the Protectora profile within Perfiles
            var protectoraProfile = usuarioProtectora.Persona.Perfiles
                .OfType<Protectora>()
                .FirstOrDefault(per => per.TipoPerfil != null && per.TipoPerfil.Id == 4);

            if (protectoraProfile == null)
            {
                return NotFound("Perfil de tipo Protectora no encontrado.");
            }

            // Explicitly load the Mascotas collection for the Protectora profile
            _context.Entry(protectoraProfile)
                .Collection(p => p.Mascotas)
                .Load();


            if (protectoraProfile.Mascotas == null || !protectoraProfile.Mascotas.Any())
            {
                return NotFound("No se encontraron mascotas asociadas al perfil de Protectora.");
            }

            // Find the specific Mascota to edit
            var mascotaToEdit = protectoraProfile.Mascotas.FirstOrDefault(m => m.Id == mascota.Id);
            if (mascotaToEdit == null)
            {
                return NotFound($"Mascota con ID {mascota.Id} no encontrada.");
            }
            if (mascotaToEdit == null)
            {
                return NotFound($"Mascota con ID {mascota.Id} no encontrada.");
            }


            mascotaToEdit.Update(
                      tipoMascota,
                      mascota.Nombre,
                      mascota.Peso,
                      mascota.AptoDepto,
                      mascota.AptoPerros,
                      mascota.FechaNacimiento,
                      mascota.Castrado,
                      mascota.Sexo,
                      mascota.Vacunado,
                      mascota.Imagen);


            // Save changes to the database
            await _context.SaveChangesAsync();

            // Map the updated Usuario to a DTO and return response
            var usuarioDto = _mapper.Map<UsuarioDto>(usuarioProtectora);

            var response = new LoginResponseDto
            {
                token = token,
                UsuarioResponseDto = usuarioDto
            };

            return Ok(ApiResponse<LoginResponseDto>.Success(response));
        }

    }
}
