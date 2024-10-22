using alumnos_api.Models;
using alumnos_api.Services.Interface;
using hogar_petfecto_api.Services;
using hogar_petfecto_api.Services.Interface;
using Microsoft.Extensions.Configuration;

namespace alumnos_api.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GestionDbContext _context;
        private readonly IConfiguration _configuration;

        public IPerfilManagerService PerfilManagerService { get; private set; }

        public IAuthService AuthService { get; private set; }

        public UnitOfWork(GestionDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

            PerfilManagerService = new PerfilManagerService(_context);
            AuthService = new AuthService(_configuration, _context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
