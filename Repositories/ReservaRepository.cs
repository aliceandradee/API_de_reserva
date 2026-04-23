using API_Reserva_de_Espaços.Data;
using API_Reserva_de_Espaços.Models;
using API_Reserva_de_Espaços.Repositories; 
using API_Reserva_de_Espaços.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API_Reserva_de_Espaços.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly AppDbContext _context;
        public ReservaRepository(AppDbContext context) => _context = context;

    
        public async Task<IEnumerable<Reserva>> ObterTodosAsync() =>
            await _context.Reservas.AsNoTracking().ToListAsync();
        public async Task<Reserva?> ObterPorIdAsync(int id) =>
            await _context.Reservas.FindAsync(id);
        public async Task<bool> VerificarSalaOcupadaAsync(string sala, DateTime data) =>
            await _context.Reservas.AnyAsync(x => x.SalaReservada == sala && x.DataReserva == data);

        public async Task AdicionarAsync(Reserva reserva)
        {
            await _context.Reservas.AddAsync(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AtualizarAsync(Reserva reserva)
        {
            _context.Entry(reserva).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var reserva = await ObterPorIdAsync(id);
            if (reserva == null) return false;

            _context.Reservas.Remove(reserva);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}