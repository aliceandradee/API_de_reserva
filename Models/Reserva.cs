namespace API_Reserva_de_Espaços.Models
{
    public class Reserva
    {
            public int Id { get; set; }
            public string NomeUsuario { get; set; }
            public string SalaReservada { get; set; }
            public DateTime DataReserva { get; set; }
            public int TempoHoras { get; set; }
        }
    }

