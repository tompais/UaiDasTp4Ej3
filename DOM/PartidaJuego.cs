namespace DOM;

public class PartidaJuego(int id, int jugadorId, DateTime fechaInicio, DateTime? fechaFin, int nivelActual, int puntajeTotal, string estado)
{
    public int Id { get; } = id;
    public int JugadorId { get; set; } = jugadorId;
    public DateTime FechaInicio { get; set; } = fechaInicio;
    public DateTime? FechaFin { get; set; } = fechaFin;
    public int NivelActual { get; set; } = nivelActual;
    public int PuntajeTotal { get; set; } = puntajeTotal;
    public string Estado { get; set; } = estado; // "EnCurso", "Finalizada", "Abandonada"
    public Jugador? Jugador { get; set; }
}
