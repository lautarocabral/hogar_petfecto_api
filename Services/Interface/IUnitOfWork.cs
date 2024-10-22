using hogar_petfecto_api.Services.Interface;

namespace alumnos_api.Services.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IPerfilManagerService PerfilManagerService { get; }
        IAuthService AuthService { get; }
        Task<int> CompleteAsync();
    }
}
