namespace DOM;

public class RespuestaJugador(int id, int partidaId, int preguntaId, int opcionSeleccionadaId, bool esCorrecta, int puntosObtenidos, DateTime fechaRespuesta)
{
    public int Id { get; } = id;
    public int PartidaId { get; set; } = partidaId;
    public int PreguntaId { get; set; } = preguntaId;
    public int OpcionSeleccionadaId { get; set; } = opcionSeleccionadaId;
    public bool EsCorrecta { get; set; } = esCorrecta;
    public int PuntosObtenidos { get; set; } = puntosObtenidos;
    public DateTime FechaRespuesta { get; set; } = fechaRespuesta;
}
