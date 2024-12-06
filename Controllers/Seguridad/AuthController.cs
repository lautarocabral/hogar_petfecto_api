using alumnos_api.Models;
using alumnos_api.Services.Interface;
using AutoMapper;
using Azure;
using hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Dtos;
using hogar_petfecto_api.Models.Dtos.Request;
using hogar_petfecto_api.Models.Dtos.Response;
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

            // Buscar usuario a modificar
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

            // Identificar grupos agregados y eliminados
            var gruposAgregados = nuevosGruposIds.Except(gruposActualesIds).ToList();
            var gruposEliminados = gruposActualesIds.Except(nuevosGruposIds).ToList();
            var gruposDistintivos = gruposAgregados;

            // Actualizar la lista de grupos del usuario
            usuarioAModificar.UpdateListOfHasToUpdateProfile(gruposDistintivos);
            var listaDeRolesNuevos = await _context.Grupos
                .Where(g => nuevosGruposIds.Contains(g.Id))
                .ToListAsync();
            usuarioAModificar.UpdateGrupos(listaDeRolesNuevos);

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            var response = new LoginResponseDto
            {
                token = token,
                UsuarioResponseDto = usuarioDto
            };

            await _context.SaveChangesAsync();

            return Ok(ApiResponse<LoginResponseDto>.Success(response));
        }


    }

}
