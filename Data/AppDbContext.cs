using API_Reserva_de_Espaços.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Reserva_de_Espaços.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Reserva> Reservas { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
