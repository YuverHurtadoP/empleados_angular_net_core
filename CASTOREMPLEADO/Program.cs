
using CASTOREMPLEADO.DAL.implementaciones;
using CASTOREMPLEADO.DAL.Interfaces;
using CASTOREMPLEADO.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CastorContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("cadenaSQL")));
builder.Services.AddControllers().AddJsonOptions(
    opt => { opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; }
    );


var misReglasCors = "misReglasCors";

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: misReglasCors, builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); 
    });
});
builder.Services.AddScoped<IEmpleadoRepositorio, EmpleadoRepositorioImpl>();
builder.Services.AddScoped<ICargoRepositorio, CargoRepositorioImpl>();
 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(misReglasCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
