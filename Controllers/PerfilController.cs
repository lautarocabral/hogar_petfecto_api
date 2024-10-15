using alumnos_api.Services.Interface;
using hogar_petfecto_api.Models.hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Perfiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hogar_petfecto_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PerfilController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Veterinaria
        [HttpGet]
        public async Task<IActionResult> GetVeterinaria([FromQuery] Veterinaria veterinaria)
        {
            if (veterinaria == null)
            {
                return BadRequest(new ApiResponse<Veterinaria>(400, "La veterinaria proporcionada es inválida o está vacía"));
            }

            var response = await _unitOfWork.PerfilManagerService.CargarVeterinaria(veterinaria);

            if (response.StatusCode == 404)
            {
                return NotFound(new ApiResponse<Veterinaria>(404, "No se encontró la veterinaria"));
            }

            if (response.StatusCode == 200)
            {
                return Ok(new ApiResponse<Veterinaria>(200, "Veterinaria obtenida con éxito", response.Result));
            }

            // Manejo de errores generales
            return StatusCode(500, new ApiResponse<Veterinaria>(500, "Ocurrió un error al obtener la veterinaria"));
        }



    }
}
