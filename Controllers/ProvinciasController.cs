using alumnos_api.Services.Interface;
using AutoMapper;
using hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Dtos;
using hogar_petfecto_api.Models.Dtos.Request;
using hogar_petfecto_api.Models.Dtos.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace hogar_petfecto_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProvinciasController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("provincias")]
        public async Task<IActionResult> GetProvincias()
        {
            var provincias = await _unitOfWork.ProvinciaService.GetProvincias();
            var provinciasDto = _mapper.Map<List<ProvinciaDto>>(provincias);

            var response = new ProvinciaResponseDto
            {
                ProvinciaDtos = provinciasDto
            };

            return Ok(ApiResponse<ProvinciaResponseDto>.Success(response));
        }

        [HttpGet("localidades/{id}")]
        public async Task<IActionResult> GetLocalidades(int id)
        {
            var localidades = await _unitOfWork.ProvinciaService.GetLocalidades(id);
            var localidadesDto = _mapper.Map<List<LocalidadDto>>(localidades);

            var response = new LocalidadResponseDto
            {
                LocalidadDtos = localidadesDto
            };

            return Ok(ApiResponse<LocalidadResponseDto>.Success(response));

        }

    }
}
