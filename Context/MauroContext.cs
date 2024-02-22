using Microsoft.EntityFrameworkCore;
using Ejemplo.Models;

namespace Ejemplo.Models
{
    public class MauroContext:DbContext
    {
        public MauroContext(DbContextOptions<MauroContext>contextOptions):base(contextOptions) { }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Orden> Orden { get; set; }
    }
}