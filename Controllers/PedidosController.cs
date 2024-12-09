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
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Font = iTextSharp.text.Font;
using hogar_petfecto_api.Models.Seguridad;

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
                        var newStock = producto.Stock - productoRequest.Cantidad;

                        producto.UpdateStock(newStock);

                        lineasPedido.Add(new LineaPedido(producto.Precio, producto, productoRequest.Cantidad));
                    }

                    // Calcular monto total
                    var montoTotal = lineasPedido.Sum(lp => (double)lp.Precio * lp.Cantidad);

                    var nroOrdenCompra = await GenerarNumeroOrdenCompra();
                    var idPago = "9501562840" + nroOrdenCompra;
                    // Crear el pedido
                    var pedido = new Pedido(
                        fecha: DateTime.Now,
                        lineaPedido: lineasPedido,
                        nroOrdenCompra: nroOrdenCompra,
                        fechaOrdenCompra: DateTime.Now,
                        idPago: idPago,
                        fechaPago: DateTime.Now,
                        monto: montoTotal * 1.15,
                        cliente: cliente,
                        protectora: protectora // Pasar el objeto Protectora aquí
                    );

                    pedidos.Add(pedido);
                }

                foreach (var p in pedidos)
                {
                    _context.Pedidos.Add(p);

                }

                _context.Events.Add(new Event(usuario.Id, "Venta", 4, DateTime.Now)); //Para auditoria

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

        private async Task<int> GenerarNumeroOrdenCompra()
        {
            var oc = await _context.OcCounts.FirstOrDefaultAsync();

            if (oc == null)
            {
                oc = new OcCount(0);
                _context.OcCounts.Add(oc);
            };

            oc.SetNroOc(oc.NroOc + 1);
            await _context.SaveChangesAsync();

            return oc.NroOc;

        }

        [HttpGet("GetHistorialVentas")]
        public async Task<IActionResult> GetHistorialVentas()
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

                var protectoraProfile = usuarioProtectora.Persona.Perfiles
                    .OfType<Protectora>()
                    .FirstOrDefault(per => per.TipoPerfil != null && per.TipoPerfil.Id == 4);

                var ventas = await _context.Pedidos
                    .Include(c => c.Cliente)
                    .Include(l => l.LineaPedido)
                    .ThenInclude(prod => prod.Producto)
                    .ThenInclude(cat => cat.Categoria)
                    .Where(p => p.Protectora.Id == protectoraProfile.Id)
                    .ToListAsync();


                var pedidosDto = _mapper.Map<List<PedidoDto>>(ventas);
                // Construye la respuesta
                var response = new PedidosResponseDto
                {
                    token = token,
                    Pedidos = pedidosDto
                };

                return Ok(ApiResponse<PedidosResponseDto>.Success(response));


            }
            catch (Exception e)
            {

                return Ok(ApiResponse<Exception>.Error(e.Message));
            }
        }


        [HttpGet("ObtenerOrdenCompra/{nroPedido}")]
        public async Task<IActionResult> ObtenerOrdenCompra(int nroPedido)
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
                return Unauthorized(ApiResponse<string>.Error("No tiene permisos para cargar un pedido", 401));
            }
            ////////////VALIDA PERMISO DE USUARIO//////////////////////////////////////////////////////////
            //AUTH/////////////////////////////////////////////////////////////////////////////////
            var filePath = @"C:\Users\lautaro\Desktop\reporteOc.pdf";
            await ObtenerReporteOrdenCompra(filePath, int.Parse(userId), nroPedido);

            var bytes = System.IO.File.ReadAllBytes(filePath);

            var base64 = Convert.ToBase64String(bytes);

            // Construye la respuesta
            var response = new OrdenCompraResponseDto
            {
                token = token,
                File = base64
            };

            return Ok(ApiResponse<OrdenCompraResponseDto>.Success(response));

        }

        private async Task<bool> ObtenerReporteOrdenCompra(string filePath, int userId, int nroPedido)
        {
            using (var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4))
            {
                try
                {
                    PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                    document.Open();

                    // Obtener el usuario protectora
                    var usuarioProtectora = await _context.Usuarios
                        .Include(u => u.Persona)
                        .ThenInclude(p => p.Perfiles)
                        .FirstOrDefaultAsync(u => u.Id == userId);

                    if (usuarioProtectora == null)
                        throw new KeyNotFoundException("Usuario no encontrado.");

                    var protectoraProfile = usuarioProtectora.Persona.Perfiles
                        .OfType<Protectora>()
                        .FirstOrDefault(per => per.TipoPerfil != null && per.TipoPerfil.Id == 4);

                    // Obtener el pedido
                    var pedido = await _context.Pedidos
                        .Include(c => c.Cliente)
                        .Include(l => l.LineaPedido)
                        .ThenInclude(prod => prod.Producto)
                        .ThenInclude(cat => cat.Categoria)
                        .FirstOrDefaultAsync(p => p.Id == nroPedido);

                    if (pedido == null)
                        throw new KeyNotFoundException("Pedido no encontrado.");

                    // Verificar que tenga líneas de pedido
                    if (pedido.LineaPedido == null || !pedido.LineaPedido.Any())
                        throw new InvalidOperationException("El pedido no contiene líneas de pedido.");

                    // Estilos de fuente
                    Font titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
                    Font headingFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
                    Font textFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);

                    // Agregar contenido al documento
                    document.Add(new Paragraph("Orden de Compra", titleFont));
                    document.Add(new Paragraph($"Número de Orden: {pedido.NroOrdenCompra}", headingFont));
                    document.Add(new Paragraph($"Cliente: {pedido.Cliente.Cuil}", textFont));
                    document.Add(new Paragraph($"Fecha: {pedido.FechaOrdenCompra:dd/MM/yyyy}", textFont));
                    document.Add(Chunk.NEWLINE);

                    // Agregar tabla con líneas de pedido
                    PdfPTable table = new PdfPTable(4); // 4 columnas: Producto, Categoría, Cantidad, Subtotal
                    table.WidthPercentage = 100;
                    table.AddCell(new PdfPCell(new Phrase("Producto", headingFont)));
                    table.AddCell(new PdfPCell(new Phrase("Categoría", headingFont)));
                    table.AddCell(new PdfPCell(new Phrase("Cantidad", headingFont)));
                    table.AddCell(new PdfPCell(new Phrase("Subtotal", headingFont)));

                    decimal total = 0m;
                    foreach (var linea in pedido.LineaPedido)
                    {
                        decimal subtotal = linea.Cantidad * linea.Producto.Precio;
                        total += subtotal;

                        table.AddCell(new PdfPCell(new Phrase(linea.Producto.Titulo, textFont)));
                        table.AddCell(new PdfPCell(new Phrase(linea.Producto.Categoria.Nombre, textFont)));
                        table.AddCell(new PdfPCell(new Phrase(linea.Cantidad.ToString(), textFont)));
                        table.AddCell(new PdfPCell(new Phrase(subtotal.ToString("C2"), textFont)));
                    }

                    // Agregar la tabla al documento
                    document.Add(table);

                    var totalPlusService = total * 1.15m;


                    // Agregar total
                    document.Add(Chunk.NEWLINE);
                    document.Add(new Paragraph($"Total + Servicio (15%): {totalPlusService.ToString("C2")}", headingFont));
                }
                catch (Exception ex)
                {
                    // Registrar el error
                    Console.WriteLine($"Error al generar el PDF: {ex.Message}");
                    throw;
                }
                finally
                {
                    // Cerrar el documento solo si tiene contenido
                    if (document.IsOpen())
                    {
                        document.Close();
                    }
                }
            }

            return true;
        }

    }
}
