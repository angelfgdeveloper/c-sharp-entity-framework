using System.ComponentModel.DataAnnotations;

namespace projectef.Models;

public class Categoria
{
    //[Key] // Etiqueta - Especificando o forzando a que sea la clave de ID
    public Guid CategoriaId { get; set; }

    //[Required] // Etiqueta - Campo requerido
    //[MaxLength(150)] // Etiqueta estricta para poner el largo del string
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int Peso { get; set; }
    public virtual ICollection<Tarea> Tareas { get; set; }
}
