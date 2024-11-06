using alumnos_api.Services.Interface;
using AutoMapper;
using hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Dtos;
using hogar_petfecto_api.Models.Dtos.Request;
using hogar_petfecto_api.Models.Dtos.Response;
using hogar_petfecto_api.Services;
using hogar_petfecto_api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace hogar_petfecto_api.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_authService = authService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var usuario = await _unitOfWork.AuthService.ValidarCredencialesAsync(request.Email, request.Password);
            if (usuario == null)
            {
                return Unauthorized(ApiResponse<string>.Error("Credenciales inválidas", 401));
            }

            var token = _unitOfWork.AuthService.GenerarToken(usuario);

            // Usa AutoMapper para mapear el usuario a UsuarioDto
            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            // Crea el LoginResponseDto
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
                return BadRequest(new { message = newUserResponse.Message });
            }

            var token = _unitOfWork.AuthService.GenerarToken(newUserResponse.Result);

            return Ok(new { token });
        }





    }

}
