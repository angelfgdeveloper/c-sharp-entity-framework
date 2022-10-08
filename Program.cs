using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using projectef;
using projectef.Models;

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

app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) => 
{
    // Lista de tareas
    // return Results.Ok(dbContext.Tareas));

    // Filtrando por Prioridad Baja
    // Where => filtrar datos
    // return Results.Ok(dbContext.Tareas.Where(t => t.PrioridadTarea == projectef.Models.Prioridad.Baja));

    // Muestra la tarea donde tiene categoria y muestra el objeto de categoria
    // Include ese objeto
    // return Results.Ok(dbContext.Tareas.Include(c => c.Categoria).Where(t => t.PrioridadTarea == projectef.Models.Prioridad.Baja));

    return Results.Ok(dbContext.Tareas.Include(c => c.Categoria));
});

app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBodyAttribute] Tarea tarea) => 
{
    tarea.TareaId = Guid.NewGuid(); // Nuevo ID
    tarea.FechaCreacion = DateTime.Now; // Fecha actual

    await dbContext.AddAsync(tarea); // #1 Agregamos el registro
    // await dbContext.Tareas.AddAsync(tarea); // #2 Agregamos el registro

    await dbContext.SaveChangesAsync(); // Confirmar que los cambios se guarden en la DB

    return Results.Ok(tarea);
});

// FromRoute => Obtener el ID en URL
app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromBodyAttribute] Tarea tarea, [FromRoute] Guid id) => 
{
    var tareaActual = dbContext.Tareas.Find(id);

    if (tareaActual != null)
    {
        tareaActual.CategoriaId = tarea.CategoriaId;
        tareaActual.Titulo = tarea.Titulo;
        tareaActual.PrioridadTarea = tarea.PrioridadTarea;
        tareaActual.Descripcion = tarea.Descripcion;

        await dbContext.SaveChangesAsync();

        return Results.Ok(tareaActual);
    }
    
    return Results.NotFound();
});

app.MapDelete("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromRoute] Guid id) => 
{
    var tareaActual = dbContext.Tareas.Find(id); // Buscar el registro actual

    if (tareaActual != null)
    {
        dbContext.Remove(tareaActual); // Eliminar
        await dbContext.SaveChangesAsync(); // Confirmar

        return Results.Ok(tareaActual);
    }
    
    return Results.NotFound();
});

app.Run();
