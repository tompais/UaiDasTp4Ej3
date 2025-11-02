using System.Data;
using Microsoft.Data.SqlClient;
using ABS;
using CONTEXT;
using DOM;

namespace REPO;

public class RepositorioJugador(DatabaseContext context) : IRepositorioJugador
{
    private readonly DatabaseContext _context = context;

    public void Agregar(Jugador entidad)
    {
        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_AgregarJugador", conexion)
        {
     CommandType = CommandType.StoredProcedure
      };

      comando.Parameters.AddWithValue("@Nombre", entidad.Nombre);
        comando.Parameters.AddWithValue("@Apellido", entidad.Apellido);
   comando.Parameters.AddWithValue("@Email", entidad.Email);

 conexion.Open();
        comando.ExecuteNonQuery();
    }

    public void Modificar(Jugador entidad)
    {
        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_ModificarJugador", conexion)
        {
     CommandType = CommandType.StoredProcedure
        };

   comando.Parameters.AddWithValue("@Id", entidad.Id);
    comando.Parameters.AddWithValue("@Nombre", entidad.Nombre);
        comando.Parameters.AddWithValue("@Apellido", entidad.Apellido);
   comando.Parameters.AddWithValue("@Email", entidad.Email);

     conexion.Open();
        comando.ExecuteNonQuery();
    }

    public void Eliminar(int id)
    {
    using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_EliminarJugador", conexion)
 {
            CommandType = CommandType.StoredProcedure
    };

    comando.Parameters.AddWithValue("@Id", id);

        conexion.Open();
        comando.ExecuteNonQuery();
    }

    public Jugador? ObtenerPorId(int id)
    {
        using var conexion = _context.CrearConexion();
 using var comando = new SqlCommand("sp_ObtenerJugadorPorId", conexion)
        {
    CommandType = CommandType.StoredProcedure
        };

        comando.Parameters.AddWithValue("@Id", id);

        conexion.Open();
        using var reader = comando.ExecuteReader();

        if (reader.Read())
        {
            return MapearJugador(reader);
      }

        return null;
    }

    public List<Jugador> ObtenerTodos()
    {
        var jugadores = new List<Jugador>();

        using var conexion = _context.CrearConexion();
   using var comando = new SqlCommand("sp_ObtenerTodosJugadores", conexion)
        {
     CommandType = CommandType.StoredProcedure
        };

      conexion.Open();
   using var reader = comando.ExecuteReader();

        while (reader.Read())
        {
 jugadores.Add(MapearJugador(reader));
        }

        return jugadores;
    }

    public Jugador? ObtenerPorEmail(string email)
    {
  using var conexion = _context.CrearConexion();
   using var comando = new SqlCommand("sp_ObtenerJugadorPorEmail", conexion)
        {
     CommandType = CommandType.StoredProcedure
        };

     comando.Parameters.AddWithValue("@Email", email);

        conexion.Open();
     using var reader = comando.ExecuteReader();

        if (reader.Read())
        {
 return MapearJugador(reader);
        }

        return null;
    }

    private static Jugador MapearJugador(SqlDataReader reader) =>
        new(
          reader.GetInt32(reader.GetOrdinal("Id")),
            reader.GetString(reader.GetOrdinal("Nombre")),
    reader.GetString(reader.GetOrdinal("Apellido")),
   reader.GetString(reader.GetOrdinal("Email"))
        );
}
