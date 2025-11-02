using DOM;

namespace ABS;

public interface IRepositorioPregunta : IRepositorioBase<Pregunta>
{
    List<Pregunta> ObtenerPorCategoria(int categoriaId);
    List<Pregunta> ObtenerPorNivel(int nivel);
    List<Pregunta> ObtenerPreguntasNoRespondidas(int partidaId, int nivel);
    Pregunta? ObtenerPreguntaCompleta(int id);
}
