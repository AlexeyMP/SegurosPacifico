using Microsoft.EntityFrameworkCore;

namespace SegurosPacifico.Models
{
    public class DbContextSalarios : DbContext
    {
        public DbContextSalarios(DbContextOptions<DbContextSalarios> options) : base(options)
        {

        }

        public DbSet<Salarios> empleados { set; get; }

        public DbSet<Usuario> Usuarios { set; get; }
    }
}
