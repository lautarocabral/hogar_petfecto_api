using alumnos_api.Services.Interface;
using hogar_petfecto_api.Models.Dtos.Request;
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

        public AuthController(IUnitOfWork unitOfWork)
        {
            //_authService = authService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var usuario = await _unitOfWork.AuthService.ValidarCredencialesAsync(request.Email, request.Password);
            if (usuario == null)
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            //var usuario = await _unitOfWork.AuthService.Login();
            //if (usuario == null)
            //{
            //    return Unauthorized(new { message = "Usuario no encontrado" });
            //}

            var token = _unitOfWork.AuthService.GenerarToken(usuario);

            return Ok(new { token });
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
