using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using projectef;

var builder = WebApplication.CreateBuilder(args);

// Creando la DB local en memoria
// builder.Services.AddDbContext<TareasContext>(opt => opt.UseInMemoryDatabase("TareasDB"));

// Creando conexión a DB en SQL server
// Obtenemos la conexion de appsettings.json con builder.Configuration.GetConnectionString("nombreDeConexion")
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Crear una base de datos en memoria
app.MapGet("/dbconnection", async ([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated(); // Asegura que esta creada la DB o sino la crea
    return Results.Ok($"Base de datos en memoria: {dbContext.Database.IsInMemory()}");
});

app.Run();
