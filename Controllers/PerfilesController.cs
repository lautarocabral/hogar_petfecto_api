using alumnos_api.Services.Interface;
using AutoMapper;
using hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Dtos;
using hogar_petfecto_api.Models.Dtos.Response;
using hogar_petfecto_api.Models.Perfiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace hogar_petfecto_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PerfilesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_authService = authService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        // POST: api/Perfiles/Adoptante
        [HttpPost("Adoptante")]
        public async Task<IActionResult> CreateAdoptante([FromBody] AdoptanteDto adoptanteDto)
        {

            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);


            if (claimsPrincipal == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));

            }

            var userId = claimsPrincipal.FindFirst("userId")?.Value;

            var usuario = await _unitOfWork.PerfilManagerService.CargarAdoptante(adoptanteDto, int.Parse(userId));

            var token = _unitOfWork.AuthService.GenerarToken(usuario);


            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            var response = new LoginResponseDto
            {
                token = token,
                UsuarioResponseDto = usuarioDto
            };

            return Ok(ApiResponse<LoginResponseDto>.Success(response));
        }
        // POST: api/Perfiles/Protectora
        [HttpPost("Protectora")]
        public async Task<IActionResult> CreateProtectora([FromBody] ProtectoraDto protectoraDto)
        {

            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);


            if (claimsPrincipal == null)
            {
                //return Ok(ApiResponse<LoginResponseDto>.UnAuthorizedToken("Token inválido o expirado."));
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));
            }

            var userId = claimsPrincipal.FindFirst("userId")?.Value;

            var usuario = await _unitOfWork.PerfilManagerService.CargarProtectora(protectoraDto, int.Parse(userId));

            var token = _unitOfWork.AuthService.GenerarToken(usuario);


            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            var response = new LoginResponseDto
            {
                token = token,
                UsuarioResponseDto = usuarioDto
            };

            return Ok(ApiResponse<LoginResponseDto>.Success(response));
        }

        // POST: api/Perfiles/Cliente
        [HttpPost("Cliente")]
        public async Task<IActionResult> CreateCliente([FromBody] ClienteDto clienteDto)
        {

            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);


            if (claimsPrincipal == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));

            }

            var userId = claimsPrincipal.FindFirst("userId")?.Value;

            var usuario = await _unitOfWork.PerfilManagerService.CargarCliente(clienteDto, int.Parse(userId));

            var token = _unitOfWork.AuthService.GenerarToken(usuario);


            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            var response = new LoginResponseDto
            {
                token = token,
                UsuarioResponseDto = usuarioDto
            };

            return Ok(ApiResponse<LoginResponseDto>.Success(response));
        }

        // POST: api/Perfiles/Cliente
        [HttpPost("Veterinaria")]
        public async Task<IActionResult> CreateVeterinaria([FromBody] VeterinariaDto veterinariaDto)
        {

            var claimsPrincipal = _unitOfWork.AuthService.GetClaimsPrincipalFromToken(HttpContext);


            if (claimsPrincipal == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Token inválido", 401));

            }

            var userId = claimsPrincipal.FindFirst("userId")?.Value;

            var usuario = await _unitOfWork.PerfilManagerService.CargarVeterinaria(veterinariaDto, int.Parse(userId));

            var token = _unitOfWork.AuthService.GenerarToken(usuario);


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
