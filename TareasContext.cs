using Microsoft.EntityFrameworkCore;
using projectef.Models;

namespace projectef;

public class TareasContext: DbContext
{
    // Los modelos se manejan en plural para referirse a la Tabla de la DB
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarea> Tareas { get; set; }

    // Configuracion general de Entity Framework
    public TareasContext(DbContextOptions<TareasContext> options): base(options) { }

    // Sobreescritura para crear los modelos - Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuracion de modelo Categoria
        modelBuilder.Entity<Categoria>(categoria => 
        {
            // Las tablas deben ir en singular
            // Validaciones de nuestros campos
            categoria.ToTable("Categoria");
            categoria.HasKey(c => c.CategoriaId);

            categoria.Property(c => c.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(c => c.Descripcion);
        });

        // Configuracion de modelo Tarea
        modelBuilder.Entity<Tarea>(tarea => // Ingresamos a cada una de las propiedades
        {
            // Las tablas deben ir en singular
            // Validaciones de nuestros campos
            tarea.ToTable("Tarea");
            tarea.HasKey(t => t.TareaId); // Asignar ID

            // Relacion entre ambos campos
            tarea.HasOne(t => t.Categoria).WithMany(c => c.Tareas).HasForeignKey(t => t.CategoriaId);

            tarea.Property(t => t.Titulo).IsRequired().HasMaxLength(200);
            tarea.Property(t => t.Descripcion);
            tarea.Property(t => t.PrioridadTarea);
            tarea.Property(t => t.FechaCreacion);

            // Ignorar campo
            tarea.Ignore(t => t.Resumen);
        });

    }
}
