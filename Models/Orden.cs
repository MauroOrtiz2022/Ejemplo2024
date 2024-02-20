using System.ComponentModel.DataAnnotations;

namespace Ejemplo.Models
{
    [Table("Ordenes")]//Se define el nombre de la tabla que se conectara
    public class Orden
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Esta linea va seguida de la llave primaria
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Numero { get; set; }
        public string Foto { get; set; } = "";
        public decimal Costo { get; set; }

        [DataType(dataType.date)]
        public DateTime Fecha { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
