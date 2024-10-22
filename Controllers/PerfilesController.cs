using alumnos_api.Services.Interface;
using hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Perfiles;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace hogar_petfecto_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PerfilesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST: api/Perfiles/Veterinaria
        [HttpPost("Veterinaria")]
        public async Task<IActionResult> CreateVeterinaria([FromQuery] int userId, [FromBody] Veterinaria veterinaria)
        {
            return await CargarPerfil(veterinaria, "Veterinaria");
        }

        // POST: api/Perfiles/Adoptante
        [HttpPost("Adoptante")]
        public async Task<IActionResult> CreateAdoptante([FromQuery] int userId, [FromBody] Adoptante adoptante)
        {
            return await CargarPerfil(adoptante, "Adoptante");
        }

        // Método genérico para cargar diferentes tipos de perfiles
        private async Task<IActionResult> CargarPerfil<T>(T perfil, string tipoPerfil) where T : class
        {
            if (perfil == null)
            {
                return BadRequest(new ApiResponse<T>(400, $"{tipoPerfil} proporcionado es inválido o está vacío"));
            }

            ApiResponse<T> response;
            switch (tipoPerfil)
            {
                case "Veterinaria":
                    response = await _unitOfWork.PerfilManagerService.CargarVeterinaria(perfil as Veterinaria) as ApiResponse<T>;
                    break;
                case "Adoptante":
                    response = await _unitOfWork.PerfilManagerService.CargarAdoptante(perfil as Adoptante) as ApiResponse<T>;
                    break;
                default:
                    return BadRequest(new ApiResponse<T>(400, "Tipo de perfil no soportado"));
            }

            if (response.StatusCode == 200)
            {
                return Ok(new ApiResponse<T>(200, $"{tipoPerfil} cargado con éxito", response.Result));
            }

            if (response.StatusCode == 404)
            {
                return NotFound(new ApiResponse<T>(404, $"No se encontró el {tipoPerfil.ToLower()}"));
            }

            return StatusCode(500, new ApiResponse<T>(500, $"Ocurrió un error al cargar el {tipoPerfil.ToLower()}"));
        }
    }
}
