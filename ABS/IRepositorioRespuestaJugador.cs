using DOM;

namespace ABS;

public interface IRepositorioRespuestaJugador : IRepositorioBase<RespuestaJugador>
{
    List<RespuestaJugador> ObtenerPorPartida(int partidaId);
}
