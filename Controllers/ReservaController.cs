using API_Reserva_de_Espaços.Models;
using API_Reserva_de_Espaços.Service;
using Microsoft.AspNetCore.Mvc;

namespace API_Reserva_de_Espaços.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaController : ControllerBase
    {
        /// <summary>
        /// Instância do ReservaService, responsável por toda a lógica de negócio relacionada às reservas.
        /// </summary>
        private readonly ReservaService _service;

        /// <summary>
        /// Constructor do ReservaController, recebe o ReservaService via Injeção de Dependência.
        /// </summary>
        /// <param name="service"></param>
        public ReservaController(ReservaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna uma lista de todas as reservas cadastradas.
        /// </summary>
        /// <returns>Lista de reservas</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var reservas = await _service.ListarReservasAsync();
            return Ok(reservas);
        }

        /// <summary>
        /// Retorna os detalhes de uma reserva específica com base no ID que o usuário deseja.
        /// </summary>
        /// <param name="id">ID da reserva</param>
        /// <returns>Objeto da reserva correspondente</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var reservas = await _service.ListarReservasAsync();
            var reserva = reservas.FirstOrDefault(r => r.Id == id);

            if (reserva == null)
                return NotFound(new { mensagem = "Reserva não encontrada." });

            return Ok(reserva);
        }

        /// <summary>
        /// Cria uma nova reserva no sistema, com base nos dados que o usuario deseja.
        /// </summary>
        /// <param name="reserva">Dados da nova reserva</param>
        /// <returns>A reserva criada com seu ID gerado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] Reserva reserva)
        {
            try
            {
                var nova = await _service.CadastrarReservaAsync(reserva);
                return CreatedAtAction(nameof(GetById), new { id = nova.Id }, nova);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados de uma reserva existente com base no ID escolhido pelo usuario.
        /// </summary>
        /// <param name="id">ID da reserva a ser atualizada</param>
        /// <param name="reserva">Dados atualizados</param>
        /// <returns>NoContent em caso de sucesso</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] Reserva reserva)
        {
            if (id != reserva.Id)
                return BadRequest(new { mensagem = "Os IDs fornecidos não coincidem." });

            var sucesso = await _service.AtualizarReservaAsync(id, reserva);

            if (!sucesso)
                return NotFound(new { mensagem = "Erro ao atualizar: Reserva não encontrada." });

            return NoContent();
        }

        /// <summary>
        /// Remove uma reserva do banco de dados com base no ID escolhido.
        /// </summary>
        /// <param name="id">ID da reserva a ser deletada</param>
        /// <returns>NoContent em caso de sucesso</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _service.DeletarReservaAsync(id);

            if (!sucesso)
                return NotFound(new { mensagem = "Reserva não encontrada para exclusão." });

            return NoContent();
        }
    }
}