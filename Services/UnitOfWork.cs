using alumnos_api.Models;
using alumnos_api.Services.Interface;
using hogar_petfecto_api.Services;
using hogar_petfecto_api.Services.Interface;

namespace alumnos_api.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GestionDbContext _context;
       
        public IPerfilManagerService PerfilManagerService { get; private set; }

        public UnitOfWork(GestionDbContext context)
        {
            _context = context;
            PerfilManagerService = new PerfilManagerService(_context);
           
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
