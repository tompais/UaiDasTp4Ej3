using ABS;
using DOM;

namespace SERV;

public class ServicioJuego(
    IRepositorioPartidaJuego repoPartida,
    IRepositorioPregunta repoPregunta,
    IRepositorioRespuestaJugador repoRespuesta,
    IRepositorioOpcionRespuesta repoOpcion)
{
    private readonly IRepositorioPartidaJuego _repoPartida = repoPartida;
    private readonly IRepositorioPregunta _repoPregunta = repoPregunta;
    private readonly IRepositorioRespuestaJugador _repoRespuesta = repoRespuesta;
    private readonly IRepositorioOpcionRespuesta _repoOpcion = repoOpcion;

    public PartidaJuego IniciarPartida(int jugadorId)
 {
   var partida = new PartidaJuego(
     0,
  jugadorId,
 DateTime.Now,
            null,
1,
      0,
  "EnCurso"
        );
        
        _repoPartida.Agregar(partida);
     return partida;
    }

    public Pregunta? ObtenerSiguientePregunta(int partidaId)
    {
        var partida = _repoPartida.ObtenerPorId(partidaId);
        if (partida == null || partida.Estado != "EnCurso")
        {
  return null;
        }

        var preguntasDisponibles = _repoPregunta.ObtenerPreguntasNoRespondidas(partidaId, partida.NivelActual);
        
        if (preguntasDisponibles.Count == 0)
    {
      return null;
        }

        var random = new Random();
    var pregunta = preguntasDisponibles[random.Next(preguntasDisponibles.Count)];
        return _repoPregunta.ObtenerPreguntaCompleta(pregunta.Id);
  }

    public (bool esCorrecta, int puntosObtenidos) RegistrarRespuesta(int partidaId, int preguntaId, int opcionSeleccionadaId)
    {
        var partida = _repoPartida.ObtenerPorId(partidaId);
        if (partida == null)
   {
 throw new InvalidOperationException("Partida no encontrada");
        }

        var opcion = _repoOpcion.ObtenerPorId(opcionSeleccionadaId);
        if (opcion == null)
        {
            throw new InvalidOperationException("Opción no encontrada");
        }

        var pregunta = _repoPregunta.ObtenerPorId(preguntaId);
        if (pregunta == null)
        {
  throw new InvalidOperationException("Pregunta no encontrada");
        }

        var esCorrecta = opcion.EsCorrecta;
        var puntosObtenidos = CalcularPuntos(esCorrecta, pregunta, partida.NivelActual);

        var respuesta = new RespuestaJugador(
      0,
            partidaId,
     preguntaId,
       opcionSeleccionadaId,
            esCorrecta,
     puntosObtenidos,
        DateTime.Now
        );

        _repoRespuesta.Agregar(respuesta);

   partida.PuntajeTotal += puntosObtenidos;
        _repoPartida.Modificar(partida);

        return (esCorrecta, puntosObtenidos);
    }

private static int CalcularPuntos(bool esCorrecta, Pregunta pregunta, int nivelActual)
    {
        if (esCorrecta)
        {
      return pregunta.PuntosAcierto;
        }

        // Nivel 4 o superior: las respuestas erróneas restan la mitad de lo que valen si acierta
        if (nivelActual >= 4 && pregunta.PuntosError.HasValue)
     {
  return pregunta.PuntosError.Value;
        }

        return 0;
    }

    public void AvanzarNivel(int partidaId)
    {
        var partida = _repoPartida.ObtenerPorId(partidaId);
        if (partida != null)
   {
            partida.NivelActual++;
         _repoPartida.Modificar(partida);
        }
    }

    public void FinalizarPartida(int partidaId)
    {
  var partida = _repoPartida.ObtenerPorId(partidaId);
        if (partida != null)
     {
            partida.Estado = "Finalizada";
  partida.FechaFin = DateTime.Now;
            _repoPartida.Modificar(partida);
        }
 }

    public void AbandonarPartida(int partidaId)
    {
        var partida = _repoPartida.ObtenerPorId(partidaId);
        if (partida != null)
        {
       partida.Estado = "Abandonada";
            partida.FechaFin = DateTime.Now;
       _repoPartida.Modificar(partida);
      }
    }
}
