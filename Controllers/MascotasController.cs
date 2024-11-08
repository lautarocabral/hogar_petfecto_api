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

        [HttpGet("GetAllMascotas")]
        public async Task<IActionResult> GetAllMascotas()
        {

            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);


            if (claimsPrincipal == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));
            }


            var protectorasYMascotas = await _context.Protectoras.Include(m => m.Mascotas).ToListAsync();
            //var protectorasDto = _mapper.Map<List<ProvinciaDto>>(provincias);

            //var protectorasDto = _mapper.Map<List<ProtectoraDto>>(protectorasYMascotas);

            var response = new MascotasResponseDto
            {
                Protectoras = protectorasYMascotas
            };

            return Ok(ApiResponse<MascotasResponseDto>.Success(response));
        }

        [HttpPost("CargaMascota")]
        public async Task<IActionResult> CargaMascota([FromBody] MascotaRequestDto mascota)
        {

            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);


            if (claimsPrincipal == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));
            }

            var userId =  claimsPrincipal.FindFirst("userId")?.Value;

            var protectora = await _context.Protectoras.Include(m => m.Mascotas).FirstOrDefaultAsync(p => p.Id == int.Parse(userId));

            var tipoMascota = await _context.TiposMascota.FirstOrDefaultAsync(t => t.Id == mascota.TipoMascota);

            protectora.Mascotas.Add(new Mascota(tipoMascota, mascota.Nombre, mascota.Peso, mascota.AptoDepto, mascota.AptoPerros, mascota.FechaNacimiento, mascota.Castrado, mascota.Sexo, mascota.Vacunado, false));

            var usuario = await _unitOfWork.AuthService.ReturnUsuario(userId);
            var token = await _unitOfWork.AuthService.GenerarToken(usuario);



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
