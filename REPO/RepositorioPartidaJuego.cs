using System.Data;
using Microsoft.Data.SqlClient;
using ABS;
using CONTEXT;
using DOM;

namespace REPO;

public class RepositorioPartidaJuego(DatabaseContext context) : IRepositorioPartidaJuego
{
    private readonly DatabaseContext _context = context;

    public void Agregar(PartidaJuego entidad)
    {
        using var conexion = _context.CrearConexion();
      using var comando = new SqlCommand("sp_AgregarPartidaJuego", conexion)
        {
    CommandType = CommandType.StoredProcedure
     };

        comando.Parameters.AddWithValue("@JugadorId", entidad.JugadorId);
        comando.Parameters.AddWithValue("@FechaInicio", entidad.FechaInicio);
        comando.Parameters.AddWithValue("@FechaFin", (object?)entidad.FechaFin ?? DBNull.Value);
        comando.Parameters.AddWithValue("@NivelActual", entidad.NivelActual);
   comando.Parameters.AddWithValue("@PuntajeTotal", entidad.PuntajeTotal);
      comando.Parameters.AddWithValue("@Estado", entidad.Estado);

        conexion.Open();
     comando.ExecuteNonQuery();
    }

    public void Modificar(PartidaJuego entidad)
    {
        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_ModificarPartidaJuego", conexion)
     {
            CommandType = CommandType.StoredProcedure
        };

 comando.Parameters.AddWithValue("@Id", entidad.Id);
     comando.Parameters.AddWithValue("@JugadorId", entidad.JugadorId);
        comando.Parameters.AddWithValue("@FechaInicio", entidad.FechaInicio);
        comando.Parameters.AddWithValue("@FechaFin", (object?)entidad.FechaFin ?? DBNull.Value);
  comando.Parameters.AddWithValue("@NivelActual", entidad.NivelActual);
        comando.Parameters.AddWithValue("@PuntajeTotal", entidad.PuntajeTotal);
  comando.Parameters.AddWithValue("@Estado", entidad.Estado);

      conexion.Open();
        comando.ExecuteNonQuery();
    }

    public void Eliminar(int id)
  {
   using var conexion = _context.CrearConexion();
      using var comando = new SqlCommand("sp_EliminarPartidaJuego", conexion)
     {
   CommandType = CommandType.StoredProcedure
};

        comando.Parameters.AddWithValue("@Id", id);

        conexion.Open();
        comando.ExecuteNonQuery();
 }

    public PartidaJuego? ObtenerPorId(int id)
    {
        using var conexion = _context.CrearConexion();
 using var comando = new SqlCommand("sp_ObtenerPartidaJuegoPorId", conexion)
      {
            CommandType = CommandType.StoredProcedure
        };

   comando.Parameters.AddWithValue("@Id", id);

    conexion.Open();
using var reader = comando.ExecuteReader();

        if (reader.Read())
      {
 return MapearPartidaJuego(reader);
      }

    return null;
    }

    public List<PartidaJuego> ObtenerTodos()
    {
        var partidas = new List<PartidaJuego>();

        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_ObtenerTodasPartidasJuego", conexion)
   {
  CommandType = CommandType.StoredProcedure
 };

        conexion.Open();
      using var reader = comando.ExecuteReader();

        while (reader.Read())
     {
partidas.Add(MapearPartidaJuego(reader));
        }

      return partidas;
    }

    public List<PartidaJuego> ObtenerPorJugador(int jugadorId)
    {
        var partidas = new List<PartidaJuego>();

using var conexion = _context.CrearConexion();
   using var comando = new SqlCommand("sp_ObtenerPartidasPorJugador", conexion)
        {
    CommandType = CommandType.StoredProcedure
        };

     comando.Parameters.AddWithValue("@JugadorId", jugadorId);

        conexion.Open();
using var reader = comando.ExecuteReader();

   while (reader.Read())
        {
         partidas.Add(MapearPartidaJuego(reader));
    }

     return partidas;
    }

 public PartidaJuego? ObtenerPartidaEnCurso(int jugadorId)
    {
 using var conexion = _context.CrearConexion();
using var comando = new SqlCommand("sp_ObtenerPartidaEnCurso", conexion)
        {
        CommandType = CommandType.StoredProcedure
      };

  comando.Parameters.AddWithValue("@JugadorId", jugadorId);

  conexion.Open();
        using var reader = comando.ExecuteReader();

        if (reader.Read())
 {
     return MapearPartidaJuego(reader);
    }

        return null;
}

 private static PartidaJuego MapearPartidaJuego(SqlDataReader reader)
    {
 var fechaFinOrdinal = reader.GetOrdinal("FechaFin");
        return new PartidaJuego(
            reader.GetInt32(reader.GetOrdinal("Id")),
 reader.GetInt32(reader.GetOrdinal("JugadorId")),
  reader.GetDateTime(reader.GetOrdinal("FechaInicio")),
  reader.IsDBNull(fechaFinOrdinal) ? null : reader.GetDateTime(fechaFinOrdinal),
   reader.GetInt32(reader.GetOrdinal("NivelActual")),
            reader.GetInt32(reader.GetOrdinal("PuntajeTotal")),
          reader.GetString(reader.GetOrdinal("Estado"))
        );
    }
}
