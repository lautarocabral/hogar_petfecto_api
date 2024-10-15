using alumnos_api.Models;
using alumnos_api.Services;
using alumnos_api.Services.Interface;
using hogar_petfecto_api.Services;
using hogar_petfecto_api.Services.Interface;
using Microsoft.EntityFrameworkCore;

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

// Database context configuration
builder.Services.AddDbContext<GestionDbContext>(options =>
{
    var config = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(config);
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IPerfilManagerService, PerfilManagerService>();

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
