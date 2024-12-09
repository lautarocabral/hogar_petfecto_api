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
using hogar_petfecto_api.Models.Dtos.Request;
using hogar_petfecto_api.Models.Seguridad;

namespace hogar_petfecto_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeterinariasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly GestionDbContext _context;

        public VeterinariasController(IUnitOfWork unitOfWork, IMapper mapper, GestionDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("GetMisSuscripciones")]
        public async Task<IActionResult> GetMisSuscripciones()
        {
            try
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
                bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 3));

                if (!hasPermiso)
                {
                    return Unauthorized(ApiResponse<string>.Error("No tiene permisos para obtener suscripciones", 401));
                }
                ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
                //AUTH/////////////////////////////////////////////////////////////////////////////////
                var usuarioVeterinaria = await _context.Usuarios
                                       .Include(u => u.Persona)
                                           .ThenInclude(p => p.Perfiles)
                                               .ThenInclude(p => (p as Veterinaria).Suscripciones)
                                       .Include(u => u.Persona)
                                           .ThenInclude(p => p.Perfiles)
                                               .ThenInclude(p => (p as Veterinaria).Ofertas)
                                       .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));



                if (usuarioVeterinaria == null || usuarioVeterinaria.Persona == null)
                {
                    return NotFound("Usuario o persona no encontrado.");
                }

                var veterinariaProfile = usuarioVeterinaria.Persona.Perfiles
                    .OfType<Veterinaria>()
                    .FirstOrDefault();

                if (veterinariaProfile == null) return Ok(ApiResponse<string>.Error("No existe el perfil veterinaria"));


                var suscripcion = veterinariaProfile.Suscripciones
                                    .OrderByDescending(s => s.Id)
                                    .FirstOrDefault();

                var suscripcionDto = _mapper.Map<SuscripcionDto>(suscripcion);

                // Construye la respuesta
                var response = new SuscripcionesResponseDto
                {
                    token = token,
                    Suscripcion = suscripcionDto
                };

                return Ok(ApiResponse<SuscripcionesResponseDto>.Success(response));


            }
            catch (Exception e)
            {

                return Ok(ApiResponse<Exception>.Error(e.Message));
            }
        }

        [HttpPost("CambiarEstadoSuscripcion")]
        public async Task<IActionResult> CambiarEstadoSuscripcion([FromBody] SuscripcionRequestDto suscripcionRequestDto)
        {
            try
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
                bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 3));

                if (!hasPermiso)
                {
                    return Unauthorized(ApiResponse<string>.Error("No tiene permisos para editar suscripciones", 401));
                }
                ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
                //AUTH/////////////////////////////////////////////////////////////////////////////////
                var usuarioVeterinaria = await _context.Usuarios
                                             .Include(u => u.Persona)
                                                 .ThenInclude(p => p.Perfiles)
                                                     .ThenInclude(p => (p as Veterinaria).Suscripciones)
                                             .Include(u => u.Persona)
                                                 .ThenInclude(p => p.Perfiles)
                                                     .ThenInclude(p => (p as Veterinaria).Ofertas)
                                             .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));


                if (usuarioVeterinaria == null || usuarioVeterinaria.Persona == null)
                {
                    return NotFound("Usuario o persona no encontrado.");
                }

                var veterinariaProfile = usuarioVeterinaria.Persona.Perfiles
                    .OfType<Veterinaria>()
                    .FirstOrDefault();

                if (veterinariaProfile == null) return Ok(ApiResponse<string>.Error("No existe el perfil veterinaria"));

                var suscripcion = veterinariaProfile.Suscripciones
                                     .OrderByDescending(s => s.Id)
                                     .FirstOrDefault();



                if (suscripcion == null) return Ok(ApiResponse<string>.Error("No existe la suscripcion"));


                if (suscripcion.Estado == false && suscripcionRequestDto.estado == true)
                {

                    var monto = 20;
                    var nuevaFechaFin = DateTime.Today.AddDays(30);
                    if (suscripcionRequestDto.tipoPlan == TipoPlan.Anual)
                    {
                        monto = 200;
                        nuevaFechaFin = nuevaFechaFin.AddDays(365);
                    }
                    // Se activa una nueva suscripcion
                    veterinariaProfile.Suscripciones.Add(
                        new Suscripcion(
                            DateTime.Today,
                            nuevaFechaFin,
                            monto,
                            suscripcionRequestDto.estado,
                            suscripcion.TipoPlan, veterinariaProfile
                            )
                        );

                }
                else
                {
                    // Se cancela suscripcion
                    suscripcion.CambiarEstado(suscripcionRequestDto.estado, DateTime.Today);
                }

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

                return Ok(ApiResponse<Exception>.Error(e.Message));
            }
        }

        [HttpPost("CambiarPlanSuscripcion")]
        public async Task<IActionResult> CambiarPlanSuscripcion([FromBody] SuscripcionRequestDto suscripcionRequestDto)
        {
            try
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
                bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 3));

                if (!hasPermiso)
                {
                    return Unauthorized(ApiResponse<string>.Error("No tiene permisos para editar suscripciones", 401));
                }
                ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
                //AUTH/////////////////////////////////////////////////////////////////////////////////
                var usuarioVeterinaria = await _context.Usuarios
                                        .Include(u => u.Persona)
                                            .ThenInclude(p => p.Perfiles)
                                                .ThenInclude(p => (p as Veterinaria).Suscripciones)
                                        .Include(u => u.Persona)
                                            .ThenInclude(p => p.Perfiles)
                                                .ThenInclude(p => (p as Veterinaria).Ofertas)
                                        .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));


                if (usuarioVeterinaria == null || usuarioVeterinaria.Persona == null)
                {
                    return NotFound("Usuario o persona no encontrado.");
                }

                var veterinariaProfile = usuarioVeterinaria.Persona.Perfiles
                    .OfType<Veterinaria>()
                    .FirstOrDefault();

                if (veterinariaProfile == null) return Ok(ApiResponse<string>.Error("No existe el perfil veterinaria"));

                var suscripcion = veterinariaProfile.Suscripciones
                                     .OrderByDescending(s => s.Id)
                                     .FirstOrDefault();



                if (suscripcion == null) return Ok(ApiResponse<string>.Error("No existe la suscripcion"));

                var monto = 20;
                var nuevaFecha = suscripcion.FechaInicio;
                if (suscripcionRequestDto.tipoPlan == TipoPlan.Anual)
                {
                    monto = 200;
                    nuevaFecha = nuevaFecha.AddDays(365);
                }

                // Se modifica el plan
                suscripcion.CambiarPlan(suscripcionRequestDto.tipoPlan, nuevaFecha);



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

                return Ok(ApiResponse<Exception>.Error(e.Message));
            }
        }


        [HttpGet("GetVeterinarias")]
        public async Task<IActionResult> GetVeterinarias()
        {
            try
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
                //////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
                //bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 3));

                //if (!hasPermiso)
                //{
                //    return Unauthorized(ApiResponse<string>.Error("No tiene permisos para editar suscripciones", 401));
                //}
                //////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
                //AUTH/////////////////////////////////////////////////////////////////////////////////
                var usuariosVeterinaria = await _context.Usuarios
                                                .Include(u => u.Persona)
                                                    .ThenInclude(p => p.Perfiles)
                                                        .ThenInclude(p => (p as Veterinaria).Suscripciones)
                                                .Include(u => u.Persona)
                                                    .ThenInclude(p => p.Perfiles)
                                                        .ThenInclude(p => (p as Veterinaria).Ofertas)
                                                .ToListAsync();

                if (usuariosVeterinaria == null || !usuariosVeterinaria.Any())
                {
                    return NotFound("Usuarios o perfiles veterinarios no encontrados.");
                }

                var perfilesVeterinarias = usuariosVeterinaria
                    .SelectMany(u => u.Persona.Perfiles)
                    .OfType<Veterinaria>().Where(s => s.Suscripciones.OrderByDescending(s => s.Id).FirstOrDefault().Estado == true) //Traigo las que tienen suscriupcion activa
                    .ToList();

                if (!perfilesVeterinarias.Any())
                {
                    return NotFound("No se encontraron perfiles de tipo Veterinaria.");
                }

                var perfilesVeterinariasDto = _mapper.Map<List<VeterinariaDto>>(perfilesVeterinarias);

                var response = new VeterinariaResponseDto
                {
                    token = token,
                    Veterinarias = perfilesVeterinariasDto
                };

                return Ok(ApiResponse<VeterinariaResponseDto>.Success(response));


            }
            catch (Exception e)
            {

                return Ok(ApiResponse<Exception>.Error(e.Message));
            }
        }

        [HttpGet("QrDecode/{dni}")]
        public async Task<IActionResult> QrDecode(string dni)
        {
            try
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
                bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 3));

                if (!hasPermiso)
                {
                    return Unauthorized(ApiResponse<string>.Error("No tiene permisos para QrDecode", 401));
                }
                ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
                //AUTH/////////////////////////////////////////////////////////////////////////////////


                var user = await _context.Usuarios.Include(p => p.Persona).FirstOrDefaultAsync(u => u.Persona.Dni == dni);

                if (user == null)
                {
                    return Ok(ApiResponse<string>.Error("Descuento Invalido"));
                }
                _context.Events.Add(new Event(usuario.Id, $"Descuento aplicado a userId {dni}", 3, DateTime.Now));

                await _context.SaveChangesAsync();

                return Ok(ApiResponse<string>.Success("QR escaneado con exito"));
            }
            catch (Exception e)
            {

                return Ok(ApiResponse<Exception>.Error(e.Message));
            }
        }

        [HttpGet("GetOfertas")]
        public async Task<IActionResult> GetOfertas()
        { 
            try
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
                bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 3));

                if (!hasPermiso)
                {
                    return Unauthorized(ApiResponse<string>.Error("No tiene permisos para QrDecode", 401));
                }
                //////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
                //AUTH/////////////////////////////////////////////////////////////////////////////////


                var ofertas = await _context.Ofertas.Include(p => p.Veterinaria).ThenInclude(p=>p.Persona).Where(u => u.Veterinaria.Persona.Dni == usuario.PersonaDni
                ).ToListAsync();

                if (ofertas == null)
                {
                    return Ok(ApiResponse<string>.Error("No se encontraron ofertas"));
                }

                var ofertasDto = _mapper.Map<List<OfertaDto>>(ofertas);

                var response = new OfertasResponseDto
                {
                    token = token,
                    Ofertas = ofertasDto
                };

                return Ok(ApiResponse<OfertasResponseDto>.Success(response));

            }
            catch (Exception e)
            {

                return Ok(ApiResponse<Exception>.Error(e.Message));
            }
        }

    }
}
