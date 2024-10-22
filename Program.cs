using alumnos_api.Models;
using alumnos_api.Services;
using alumnos_api.Services.Interface;
using hogar_petfecto_api.Services;
using hogar_petfecto_api.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS policy name
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                      });
});


// JWT implementation
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "basilio_issuer",
        ValidAudience = "basilio_audience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tu_clave_secreta"))
    };
});


// Database context configuration
builder.Services.AddDbContext<GestionDbContext>(options =>
{
    var config = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(config);
});
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPerfilManagerService, PerfilManagerService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins); 

app.UseAuthorization();

app.MapControllers();

app.Run();
