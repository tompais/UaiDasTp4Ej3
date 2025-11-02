namespace DOM;

public class Categoria(int id, string nombre, string descripcion)
{
  public int Id { get; } = id;
    public string Nombre { get; set; } = nombre;
    public string Descripcion { get; set; } = descripcion;
}
