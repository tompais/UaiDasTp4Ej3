using DOM;

namespace ABS;

public interface IRepositorioJugador : IRepositorioBase<Jugador>
{
    Jugador? ObtenerPorEmail(string email);
}
