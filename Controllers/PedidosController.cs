// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using hogar_petfecto_api.Models.Dtos.Request;
using hogar_petfecto_api.Models.Dtos.Response;
using hogar_petfecto_api.Models.Dtos;
using hogar_petfecto_api.Models.Perfiles;
using hogar_petfecto_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using alumnos_api.Models;
using alumnos_api.Services.Interface;
using AutoMapper;
using Azure.Core;

namespace hogar_petfecto_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly GestionDbContext _context;

        public PedidosController(IUnitOfWork unitOfWork, IMapper mapper, GestionDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }



        [HttpPost("CargarPedido")]
        public async Task<IActionResult> CargarPedido(PedidoRequestDto pedidoRequestDto)
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
                bool hasPermiso = usuario.Grupos.Any(grupo => grupo.Permisos.Any(p => p.Id == 2));

                if (!hasPermiso)
                {
                    return Unauthorized(ApiResponse<string>.Error("No tiene permisos para cargar un pedido", 401));
                }
                ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
                //AUTH/////////////////////////////////////////////////////////////////////////////////

                var cliente = usuario.Persona.Perfiles.OfType<Cliente>().FirstOrDefault();

                var pedidos = new List<Pedido>();

                var usuarioWithProtectora = await _context.Usuarios
                    .Include(u => u.Persona)
                    .ThenInclude(p => p.Perfiles)
                    .ToListAsync();

                foreach (var pedidoRequest in pedidoRequestDto.Pedidos)
                {
                    // Obtener la protectora desde el ID
                    var protectoraUsuario = usuarioWithProtectora.FirstOrDefault(u =>
                        u.Persona.Perfiles.OfType<Protectora>().Any(pr => pr.Id == pedidoRequest.ProtectoraId));

                    if (protectoraUsuario == null)
                    {
                        throw new Exception($"No se encontró la protectora con ID {pedidoRequest.ProtectoraId}");
                    }

                    // Extraer el perfil Protectora
                    var protectora = protectoraUsuario.Persona.Perfiles
                        .OfType<Protectora>()
                        .FirstOrDefault(pr => pr.Id == pedidoRequest.ProtectoraId);

                    if (protectora == null)
                    {
                        throw new Exception($"No se encontró el perfil Protectora con ID {pedidoRequest.ProtectoraId}");
                    }

                    // Crear las líneas de pedido
                    var lineasPedido = new List<LineaPedido>();
                    foreach (var productoRequest in pedidoRequest.Productos)
                    {
                        var producto = await _context.Productos.FirstOrDefaultAsync(prod => prod.Id == productoRequest.Id);

                        if (producto == null)
                        {
                            throw new Exception($"No se encontró el producto con ID {productoRequest.Id}");
                        }

                        lineasPedido.Add(new LineaPedido(producto.Precio, producto, 1)); // Asume cantidad = 1
                    }

                    // Calcular monto total
                    var montoTotal = lineasPedido.Sum(lp => (double)lp.Precio);

                    // Crear el pedido
                    var pedido = new Pedido(
                        fecha: DateTime.Now,
                        lineaPedido: lineasPedido,
                        nroOrdenCompra: GenerarNumeroOrdenCompra(),
                        fechaOrdenCompra: DateTime.Now,
                        idPago: GenerarIdPago(),
                        fechaPago: DateTime.Now,
                        monto: montoTotal,
                        cliente: cliente,
                        protectora: protectora // Pasar el objeto Protectora aquí
                    );

                    pedidos.Add(pedido);
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
    }
}
