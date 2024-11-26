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
using Azure;

namespace hogar_petfecto_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchandisingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly GestionDbContext _context;

        public MerchandisingController(IUnitOfWork unitOfWork, IMapper mapper, GestionDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost("CargaMerchandising")]
        public async Task<IActionResult> CargaMerchandising(MerchandisingRequestDto merchandisingRequestDto)
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
                bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 4));

                if (!hasPermiso)
                {
                    return Unauthorized(ApiResponse<string>.Error("No tiene permisos para cargar una merch", 401));
                }
                ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
                //AUTH/////////////////////////////////////////////////////////////////////////////////

                var usuarioProtectora = await _context.Usuarios
                    .Include(u => u.Persona)
                        .ThenInclude(p => p.Perfiles)
                    .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));

                if (usuarioProtectora == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                // Filtrar el perfil para obtener el perfil de tipo Protectora
                var adoptantePerfil = usuario.Persona.Perfiles
                    .OfType<Protectora>()
                    .FirstOrDefault();

                var categoria = await _context.Categorias.FirstOrDefaultAsync(id => id.Id == merchandisingRequestDto.CategoriaId);
                if (adoptantePerfil == null)
                {
                    return Ok(ApiResponse<string>.Error("No existe el adoptante"));
                }
                if (categoria == null)
                {
                    return Ok(ApiResponse<string>.Error("No existe la categoria"));
                }

                var newProducto = new Producto(
                  merchandisingRequestDto.Descripcion, merchandisingRequestDto.Stock, merchandisingRequestDto.Precio, categoria, merchandisingRequestDto.Imagen);

                adoptantePerfil.Productos.Add(newProducto);

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

        [HttpPost("EditarMerchandising")]
        public async Task<IActionResult> EditarMerchandising(MerchandisingRequestDto merchandisingRequestDto)
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
                bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 4));

                if (!hasPermiso)
                {
                    return Unauthorized(ApiResponse<string>.Error("No tiene permisos para editar merch", 401));
                }
                ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
                //AUTH/////////////////////////////////////////////////////////////////////////////////

                var usuarioProtectora = await _context.Usuarios
                    .Include(u => u.Persona)
                        .ThenInclude(p => p.Perfiles)
                    .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));

                if (usuarioProtectora == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                // Filtrar el perfil para obtener el perfil de tipo Protectora
                var adoptantePerfil = usuario.Persona.Perfiles
                    .OfType<Protectora>()
                    .FirstOrDefault();

                var categoria = await _context.Categorias.FirstOrDefaultAsync(id => id.Id == merchandisingRequestDto.CategoriaId);
                if (adoptantePerfil == null)
                {
                    return Ok(ApiResponse<string>.Error("No existe el adoptante"));
                }
                if (categoria == null)
                {
                    return Ok(ApiResponse<string>.Error("No existe la categoria"));
                }



                var producto = adoptantePerfil.Productos.FirstOrDefault(prod => prod.Id == merchandisingRequestDto.ProductoId);

                producto.Update(merchandisingRequestDto.Descripcion, merchandisingRequestDto.Stock, merchandisingRequestDto.Precio, categoria, merchandisingRequestDto.Imagen);

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

        [HttpGet("GetMerchandisingProtectora")]
        public async Task<IActionResult> GetMerchandisinProtectora()
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
                bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 4));

                if (!hasPermiso)
                {
                    return Unauthorized(ApiResponse<string>.Error("No tiene permisos para editar merch", 401));
                }
                ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
                //AUTH/////////////////////////////////////////////////////////////////////////////////
                var usuarioProtectora = await _context.Usuarios
                          .Include(u => u.Persona)
                              .ThenInclude(p => p.Perfiles)
                          .ThenInclude(p => ((Protectora)p).Productos).ThenInclude(t => t.Categoria)
                          .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));

                if (usuarioProtectora == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                // Filtrar el perfil para obtener el perfil de tipo Protectora

                var protectoraProfile = usuarioProtectora.Persona.Perfiles
                        .OfType<Protectora>()
                        .FirstOrDefault();

                if (protectoraProfile == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                var merchandising = protectoraProfile.Productos;

                var merchandisingDto = _mapper.Map<List<ProductoDto>>(merchandising);

                var response = new ProductosResponseDto
                {
                    token = token,
                    Productos = merchandisingDto
                };


                return Ok(ApiResponse<ProductosResponseDto>.Success(response));

            }
            catch (Exception e)
            {

                return Ok(ApiResponse<Exception>.Error(e.Message));
            }
        }

        [HttpGet("GetCategorias")]
        public async Task<IActionResult> GetCategorias()
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
                bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 4));

                if (!hasPermiso)
                {
                    return Unauthorized(ApiResponse<string>.Error("No tiene permisos para editar merch", 401));
                }
                ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
                //AUTH/////////////////////////////////////////////////////////////////////////////////
                var categorias = await _context.Categorias.ToListAsync();

                if (categorias == null)
                {
                    throw new Exception("Categorias no encontradas");
                }

                var categoriasDtos = _mapper.Map<List<CategoriaDto>>(categorias);

                var response = new CategoriasResponseDto
                {
                    token = token,
                    Categorias = categoriasDtos
                };


                return Ok(ApiResponse<CategoriasResponseDto>.Success(response));

            }
            catch (Exception e)
            {

                return Ok(ApiResponse<Exception>.Error(e.Message));
            }
        }
    }
}
