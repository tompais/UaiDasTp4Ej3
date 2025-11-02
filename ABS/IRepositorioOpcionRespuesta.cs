using DOM;

namespace ABS;

public interface IRepositorioOpcionRespuesta : IRepositorioBase<OpcionRespuesta>
{
    List<OpcionRespuesta> ObtenerPorPregunta(int preguntaId);
}
