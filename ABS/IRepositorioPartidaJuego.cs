using DOM;

namespace ABS;

public interface IRepositorioPartidaJuego : IRepositorioBase<PartidaJuego>
{
    List<PartidaJuego> ObtenerPorJugador(int jugadorId);
    PartidaJuego? ObtenerPartidaEnCurso(int jugadorId);
}
