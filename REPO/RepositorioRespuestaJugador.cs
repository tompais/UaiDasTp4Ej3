using System.Data;
using Microsoft.Data.SqlClient;
using ABS;
using CONTEXT;
using DOM;

namespace REPO;

public class RepositorioRespuestaJugador(DatabaseContext context) : IRepositorioRespuestaJugador
{
    private readonly DatabaseContext _context = context;

    public void Agregar(RespuestaJugador entidad)
    {
        using var conexion = _context.CrearConexion();
 using var comando = new SqlCommand("sp_AgregarRespuestaJugador", conexion)
   {
 CommandType = CommandType.StoredProcedure
        };

        comando.Parameters.AddWithValue("@PartidaId", entidad.PartidaId);
     comando.Parameters.AddWithValue("@PreguntaId", entidad.PreguntaId);
   comando.Parameters.AddWithValue("@OpcionSeleccionadaId", entidad.OpcionSeleccionadaId);
        comando.Parameters.AddWithValue("@EsCorrecta", entidad.EsCorrecta);
        comando.Parameters.AddWithValue("@PuntosObtenidos", entidad.PuntosObtenidos);
        comando.Parameters.AddWithValue("@FechaRespuesta", entidad.FechaRespuesta);

  conexion.Open();
    comando.ExecuteNonQuery();
    }

    public void Modificar(RespuestaJugador entidad)
    {
   using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_ModificarRespuestaJugador", conexion)
   {
            CommandType = CommandType.StoredProcedure
        };

    comando.Parameters.AddWithValue("@Id", entidad.Id);
   comando.Parameters.AddWithValue("@PartidaId", entidad.PartidaId);
     comando.Parameters.AddWithValue("@PreguntaId", entidad.PreguntaId);
     comando.Parameters.AddWithValue("@OpcionSeleccionadaId", entidad.OpcionSeleccionadaId);
     comando.Parameters.AddWithValue("@EsCorrecta", entidad.EsCorrecta);
  comando.Parameters.AddWithValue("@PuntosObtenidos", entidad.PuntosObtenidos);
 comando.Parameters.AddWithValue("@FechaRespuesta", entidad.FechaRespuesta);

        conexion.Open();
      comando.ExecuteNonQuery();
  }

    public void Eliminar(int id)
    {
     using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_EliminarRespuestaJugador", conexion)
        {
        CommandType = CommandType.StoredProcedure
      };

     comando.Parameters.AddWithValue("@Id", id);

        conexion.Open();
        comando.ExecuteNonQuery();
    }

    public RespuestaJugador? ObtenerPorId(int id)
  {
 using var conexion = _context.CrearConexion();
      using var comando = new SqlCommand("sp_ObtenerRespuestaJugadorPorId", conexion)
{
      CommandType = CommandType.StoredProcedure
   };

   comando.Parameters.AddWithValue("@Id", id);

        conexion.Open();
    using var reader = comando.ExecuteReader();

        if (reader.Read())
   {
 return MapearRespuestaJugador(reader);
        }

 return null;
    }

    public List<RespuestaJugador> ObtenerTodos()
    {
  var respuestas = new List<RespuestaJugador>();

        using var conexion = _context.CrearConexion();
  using var comando = new SqlCommand("sp_ObtenerTodasRespuestasJugador", conexion)
  {
   CommandType = CommandType.StoredProcedure
     };

        conexion.Open();
   using var reader = comando.ExecuteReader();

     while (reader.Read())
        {
       respuestas.Add(MapearRespuestaJugador(reader));
        }

   return respuestas;
    }

    public List<RespuestaJugador> ObtenerPorPartida(int partidaId)
    {
        var respuestas = new List<RespuestaJugador>();

      using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_ObtenerRespuestasPorPartida", conexion)
    {
            CommandType = CommandType.StoredProcedure
        };

        comando.Parameters.AddWithValue("@PartidaId", partidaId);

        conexion.Open();
        using var reader = comando.ExecuteReader();

    while (reader.Read())
 {
   respuestas.Add(MapearRespuestaJugador(reader));
        }

        return respuestas;
    }

    private static RespuestaJugador MapearRespuestaJugador(SqlDataReader reader) =>
    new(
       reader.GetInt32(reader.GetOrdinal("Id")),
            reader.GetInt32(reader.GetOrdinal("PartidaId")),
       reader.GetInt32(reader.GetOrdinal("PreguntaId")),
  reader.GetInt32(reader.GetOrdinal("OpcionSeleccionadaId")),
       reader.GetBoolean(reader.GetOrdinal("EsCorrecta")),
         reader.GetInt32(reader.GetOrdinal("PuntosObtenidos")),
  reader.GetDateTime(reader.GetOrdinal("FechaRespuesta"))
 );
}
