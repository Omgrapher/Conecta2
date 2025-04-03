using ESP8266.Models;
using Microsoft.EntityFrameworkCore;

namespace conecta2.BD
{
        public class ApplicationDbContext : DbContext // Asegúrate de que hereda de DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            public DbSet<Usuario> Usuarios { get; set; } // Añade tus DbSets aquí
        }
}
