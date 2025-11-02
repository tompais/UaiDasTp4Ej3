namespace DOM;

public class Pregunta(int id, string textoPregunta, int categoriaId, int nivel, int puntosAcierto, int? puntosError)
{
    public int Id { get; } = id;
  public string TextoPregunta { get; set; } = textoPregunta;
    public int CategoriaId { get; set; } = categoriaId;
  public int Nivel { get; set; } = nivel;
    public int PuntosAcierto { get; set; } = puntosAcierto;
    public int? PuntosError { get; set; } = puntosError;
    public Categoria? Categoria { get; set; }
    public List<OpcionRespuesta> Opciones { get; set; } = [];
}
