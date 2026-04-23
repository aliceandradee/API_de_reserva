using API_Reserva_de_Espaços.Models;
using API_Reserva_de_Espaços.Repositories;
using API_Reserva_de_Espaços.Repositories.Interface;

namespace API_Reserva_de_Espaços.Service
{
    public class ReservaService
    {
        private readonly IReservaRepository _repository;

        public ReservaService(IReservaRepository repository) => _repository = repository;

        // Método para listar todas as reservas
        public async Task<IEnumerable<Reserva>> ListarReservasAsync() => await _repository.ObterTodosAsync();

        // Método para cadastrar uma nova reserva 
        public async Task<Reserva> CadastrarReservaAsync(Reserva reserva)
        {
            // Validação: Verifica se a sala já está ocupada naquele horário (lógica usada na minha API da biblioteca sobre o ISBN)
            if (await _repository.VerificarSalaOcupadaAsync(reserva.SalaReservada, reserva.DataReserva))
                throw new Exception("Esta sala já possui uma reserva para este horário!");

            await _repository.AdicionarAsync(reserva);
            return reserva;
        }

        // Método para atualizar uma reserva se já existir
        public async Task<bool> AtualizarReservaAsync(int id, Reserva reserva)
        {
            if (id != reserva.Id) return false;
            return await _repository.AtualizarAsync(reserva);
        }

        // Método para deletar uma reserva
        public async Task<bool> DeletarReservaAsync(int id) => await _repository.DeletarAsync(id);
    }
}