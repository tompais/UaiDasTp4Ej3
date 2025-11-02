namespace DOM;

public class Jugador(int id, string nombre, string apellido, string email)
{
    public int Id { get; } = id;
    public string Nombre { get; set; } = nombre;
    public string Apellido { get; set; } = apellido;
    public string Email { get; set; } = email;
}
