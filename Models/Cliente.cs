using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ejemplo.Models
{
    [Table("Clientes")]//Se define el nombre de la tabla que se conectara
    public class Cliente
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Esta linea va seguida de la llave primaria
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Mesa { get; set; }
    }
}
