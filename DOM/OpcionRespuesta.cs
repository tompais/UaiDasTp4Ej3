namespace DOM;

public class OpcionRespuesta(int id, int preguntaId, string textoOpcion, bool esCorrecta)
{
 public int Id { get; } = id;
public int PreguntaId { get; set; } = preguntaId;
    public string TextoOpcion { get; set; } = textoOpcion;
    public bool EsCorrecta { get; set; } = esCorrecta;
}
