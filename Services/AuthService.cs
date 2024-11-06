using alumnos_api.Models;
using hogar_petfecto_api.Models;
using hogar_petfecto_api.Models.Dtos.Request;
using hogar_petfecto_api.Models.Seguridad;
using hogar_petfecto_api.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace hogar_petfecto_api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly GestionDbContext _context;

        public AuthService(IConfiguration configuration, GestionDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }


        public string GenerarToken(Usuario usuario)
        {
            var key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key) || key.Length < 32)
            {
                throw new ArgumentException("La clave JWT debe tener al menos 32 caracteres.");
            }

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
        new Claim("userId", usuario.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var keyBytes = Encoding.UTF8.GetBytes(key);
            var creds = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddYears(100),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public async Task<ApiResponse<Usuario>> Login(Usuario usuario)
        {

            throw new NotImplementedException();
        }
        public async Task<ApiResponse<Usuario>> SignUp(SignUpRequestDto signUpDtoRequest)
        {
            // 1. Verificar si ya existe un usuario con el mismo Dni
            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.PersonaDni == signUpDtoRequest.Dni);

            if (usuarioExistente != null)
            {
                return ApiResponse<Usuario>.Error("Ya existe un usuario registrado con este Dni.");
            }

            var grupo = await _context.Grupos.FirstOrDefaultAsync(g => g.Id == 2); // el usuario se registra con id 2 que corresponde a Invitado
            if (grupo == null)
            {
                throw new KeyNotFoundException("Grupo no encontrado.");
            }

            List<Grupo> grupos = new List<Grupo> { grupo };

            var nuevaLocalidad = await _context.Localidades.FirstOrDefaultAsync(l => l.Id == signUpDtoRequest.LocalidadId);

            var nuevoUsuario = new Usuario(signUpDtoRequest.Email, HashPassword(signUpDtoRequest.Password), grupos, new Persona(signUpDtoRequest.Dni, signUpDtoRequest.RazonSocial, nuevaLocalidad, signUpDtoRequest.Direccion, signUpDtoRequest.Telefono, signUpDtoRequest.FechaNacimiento, new List<Perfil>()));

            _context.Usuarios.Add(nuevoUsuario);

            await _context.SaveChangesAsync();

            return ApiResponse<Usuario>.Success(nuevoUsuario, "SignUp exitoso.");
        }






        public async Task<Usuario?> ValidarCredencialesAsync(string email, string contraseña)
        {
            // Busca el usuario en la base de datos por email
            var usuarioExistente = await _context.Usuarios
    .Include(u => u.Persona)
        .ThenInclude(p => p.Localidad)
            .ThenInclude(l => l.Provincia)
    .Include(u => u.Persona)
        .ThenInclude(p => p.Perfiles)
            .ThenInclude(perfil => perfil.TipoPerfil) 
    .Include(u => u.Grupos)
        .ThenInclude(g => g.Permisos)
    .FirstOrDefaultAsync(u => u.Email == email);


            // Si el usuario no existe o la contraseña es incorrecta, retorna null
            if (usuarioExistente == null || !VerifyPassword(contraseña, usuarioExistente.Contraseña))
            {
                return null;
            }

            // Si las credenciales son válidas, retorna el objeto Usuario
            return usuarioExistente;
        }


        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
