using API_Reserva_de_Espaços.Models;

namespace API_Reserva_de_Espaços.Repositories.Interface
{
    public interface IReservaRepository
    {
        Task AdicionarAsync(Reserva reserva);
        Task<bool> AtualizarAsync(Reserva reserva);
        Task<bool> DeletarAsync(int id);
        Task<IEnumerable<Reserva>> ObterTodosAsync();
        Task<bool> VerificarSalaOcupadaAsync(string salaReservada, DateTime dataReserva);
    }
}
