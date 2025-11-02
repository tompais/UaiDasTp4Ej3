using System.Data;
using Microsoft.Data.SqlClient;
using ABS;
using CONTEXT;
using DOM;

namespace REPO;

public class RepositorioPregunta(DatabaseContext context) : IRepositorioPregunta
{
    private readonly DatabaseContext _context = context;

    public void Agregar(Pregunta entidad)
    {
   using var conexion = _context.CrearConexion();
  using var comando = new SqlCommand("sp_AgregarPregunta", conexion)
        {
   CommandType = CommandType.StoredProcedure
        };

        comando.Parameters.AddWithValue("@TextoPregunta", entidad.TextoPregunta);
        comando.Parameters.AddWithValue("@CategoriaId", entidad.CategoriaId);
 comando.Parameters.AddWithValue("@Nivel", entidad.Nivel);
    comando.Parameters.AddWithValue("@PuntosAcierto", entidad.PuntosAcierto);
   comando.Parameters.AddWithValue("@PuntosError", (object?)entidad.PuntosError ?? DBNull.Value);

        conexion.Open();
        comando.ExecuteNonQuery();
    }

  public void Modificar(Pregunta entidad)
    {
        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_ModificarPregunta", conexion)
        {
      CommandType = CommandType.StoredProcedure
        };

      comando.Parameters.AddWithValue("@Id", entidad.Id);
        comando.Parameters.AddWithValue("@TextoPregunta", entidad.TextoPregunta);
        comando.Parameters.AddWithValue("@CategoriaId", entidad.CategoriaId);
    comando.Parameters.AddWithValue("@Nivel", entidad.Nivel);
        comando.Parameters.AddWithValue("@PuntosAcierto", entidad.PuntosAcierto);
        comando.Parameters.AddWithValue("@PuntosError", (object?)entidad.PuntosError ?? DBNull.Value);

        conexion.Open();
     comando.ExecuteNonQuery();
    }

    public void Eliminar(int id)
    {
     using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_EliminarPregunta", conexion)
        {
         CommandType = CommandType.StoredProcedure
        };

        comando.Parameters.AddWithValue("@Id", id);

        conexion.Open();
        comando.ExecuteNonQuery();
    }

    public Pregunta? ObtenerPorId(int id)
    {
        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_ObtenerPreguntaPorId", conexion)
        {
            CommandType = CommandType.StoredProcedure
        };

        comando.Parameters.AddWithValue("@Id", id);

  conexion.Open();
        using var reader = comando.ExecuteReader();

        if (reader.Read())
        {
            return MapearPregunta(reader);
     }

        return null;
    }

    public List<Pregunta> ObtenerTodos()
    {
        var preguntas = new List<Pregunta>();

        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_ObtenerTodasPreguntas", conexion)
        {
            CommandType = CommandType.StoredProcedure
        };

        conexion.Open();
        using var reader = comando.ExecuteReader();

        while (reader.Read())
        {
preguntas.Add(MapearPregunta(reader));
        }

    return preguntas;
    }

    public List<Pregunta> ObtenerPorCategoria(int categoriaId)
    {
        var preguntas = new List<Pregunta>();

        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_ObtenerPreguntasPorCategoria", conexion)
        {
            CommandType = CommandType.StoredProcedure
        };

 comando.Parameters.AddWithValue("@CategoriaId", categoriaId);

        conexion.Open();
      using var reader = comando.ExecuteReader();

        while (reader.Read())
  {
      preguntas.Add(MapearPregunta(reader));
}

        return preguntas;
    }

    public List<Pregunta> ObtenerPorNivel(int nivel)
    {
    var preguntas = new List<Pregunta>();

        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_ObtenerPreguntasPorNivel", conexion)
 {
       CommandType = CommandType.StoredProcedure
        };

      comando.Parameters.AddWithValue("@Nivel", nivel);

        conexion.Open();
   using var reader = comando.ExecuteReader();

        while (reader.Read())
        {
            preguntas.Add(MapearPregunta(reader));
        }

     return preguntas;
    }

    public List<Pregunta> ObtenerPreguntasNoRespondidas(int partidaId, int nivel)
  {
        var preguntas = new List<Pregunta>();

        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_ObtenerPreguntasNoRespondidas", conexion)
  {
    CommandType = CommandType.StoredProcedure
        };

        comando.Parameters.AddWithValue("@PartidaId", partidaId);
        comando.Parameters.AddWithValue("@Nivel", nivel);

        conexion.Open();
        using var reader = comando.ExecuteReader();

        while (reader.Read())
      {
            preguntas.Add(MapearPregunta(reader));
        }

        return preguntas;
  }

  public Pregunta? ObtenerPreguntaCompleta(int id)
    {
        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_ObtenerPreguntaCompleta", conexion)
    {
            CommandType = CommandType.StoredProcedure
        };

        comando.Parameters.AddWithValue("@Id", id);

        conexion.Open();
        using var reader = comando.ExecuteReader();

  Pregunta? pregunta = null;

        while (reader.Read())
        {
  if (pregunta == null)
            {
            pregunta = MapearPregunta(reader);
        }

     if (!reader.IsDBNull(reader.GetOrdinal("OpcionId")))
         {
  var opcion = new OpcionRespuesta(
       reader.GetInt32(reader.GetOrdinal("OpcionId")),
     reader.GetInt32(reader.GetOrdinal("Id")),
           reader.GetString(reader.GetOrdinal("TextoOpcion")),
          reader.GetBoolean(reader.GetOrdinal("EsCorrecta"))
    );
                pregunta.Opciones.Add(opcion);
            }
        }

        return pregunta;
    }

    private static Pregunta MapearPregunta(SqlDataReader reader)
    {
        var puntosErrorOrdinal = reader.GetOrdinal("PuntosError");
 return new Pregunta(
   reader.GetInt32(reader.GetOrdinal("Id")),
       reader.GetString(reader.GetOrdinal("TextoPregunta")),
    reader.GetInt32(reader.GetOrdinal("CategoriaId")),
   reader.GetInt32(reader.GetOrdinal("Nivel")),
            reader.GetInt32(reader.GetOrdinal("PuntosAcierto")),
            reader.IsDBNull(puntosErrorOrdinal) ? null : reader.GetInt32(puntosErrorOrdinal)
 );
    }
}
