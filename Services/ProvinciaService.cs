using alumnos_api.Models;
using hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Dtos;
using hogar_petfecto_api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace hogar_petfecto_api.Services
{
    public class ProvinciaService : IProvinciaService
    {
        private readonly IConfiguration _configuration;
        private readonly GestionDbContext _context;

        public ProvinciaService(IConfiguration configuration, GestionDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<List<Localidad?>> GetLocalidades(int id)
        {
            var localidades = await _context.Localidades.Where( p => p.ProvinciaId == id).ToListAsync();
            return localidades;

        }
        public async Task<List<Provincia>> GetProvincias()
        {
            var provincias = await _context.Provincias.ToListAsync();
            return provincias;
        }

    }
}
