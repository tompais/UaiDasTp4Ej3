using System.Data;
using Microsoft.Data.SqlClient;
using ABS;
using CONTEXT;
using DOM;

namespace REPO;

public class RepositorioOpcionRespuesta(DatabaseContext context) : IRepositorioOpcionRespuesta
{
    private readonly DatabaseContext _context = context;

    public void Agregar(OpcionRespuesta entidad)
    {
        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_AgregarOpcionRespuesta", conexion)
        {
     CommandType = CommandType.StoredProcedure
     };

        comando.Parameters.AddWithValue("@PreguntaId", entidad.PreguntaId);
   comando.Parameters.AddWithValue("@TextoOpcion", entidad.TextoOpcion);
    comando.Parameters.AddWithValue("@EsCorrecta", entidad.EsCorrecta);

      conexion.Open();
   comando.ExecuteNonQuery();
    }

    public void Modificar(OpcionRespuesta entidad)
    {
      using var conexion = _context.CrearConexion();
    using var comando = new SqlCommand("sp_ModificarOpcionRespuesta", conexion)
        {
          CommandType = CommandType.StoredProcedure
        };

        comando.Parameters.AddWithValue("@Id", entidad.Id);
     comando.Parameters.AddWithValue("@PreguntaId", entidad.PreguntaId);
        comando.Parameters.AddWithValue("@TextoOpcion", entidad.TextoOpcion);
    comando.Parameters.AddWithValue("@EsCorrecta", entidad.EsCorrecta);

   conexion.Open();
        comando.ExecuteNonQuery();
    }

    public void Eliminar(int id)
    {
        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_EliminarOpcionRespuesta", conexion)
        {
 CommandType = CommandType.StoredProcedure
     };

        comando.Parameters.AddWithValue("@Id", id);

        conexion.Open();
        comando.ExecuteNonQuery();
    }

    public OpcionRespuesta? ObtenerPorId(int id)
    {
        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_ObtenerOpcionRespuestaPorId", conexion)
        {
     CommandType = CommandType.StoredProcedure
        };

        comando.Parameters.AddWithValue("@Id", id);

        conexion.Open();
     using var reader = comando.ExecuteReader();

    if (reader.Read())
        {
            return MapearOpcionRespuesta(reader);
        }

        return null;
    }

    public List<OpcionRespuesta> ObtenerTodos()
 {
        var opciones = new List<OpcionRespuesta>();

        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_ObtenerTodasOpcionesRespuesta", conexion)
   {
            CommandType = CommandType.StoredProcedure
        };

        conexion.Open();
        using var reader = comando.ExecuteReader();

        while (reader.Read())
        {
        opciones.Add(MapearOpcionRespuesta(reader));
      }

    return opciones;
    }

    public List<OpcionRespuesta> ObtenerPorPregunta(int preguntaId)
    {
     var opciones = new List<OpcionRespuesta>();

        using var conexion = _context.CrearConexion();
      using var comando = new SqlCommand("sp_ObtenerOpcionesPorPregunta", conexion)
     {
            CommandType = CommandType.StoredProcedure
        };

    comando.Parameters.AddWithValue("@PreguntaId", preguntaId);

        conexion.Open();
   using var reader = comando.ExecuteReader();

        while (reader.Read())
      {
            opciones.Add(MapearOpcionRespuesta(reader));
        }

        return opciones;
    }

    private static OpcionRespuesta MapearOpcionRespuesta(SqlDataReader reader) =>
        new(
reader.GetInt32(reader.GetOrdinal("Id")),
   reader.GetInt32(reader.GetOrdinal("PreguntaId")),
     reader.GetString(reader.GetOrdinal("TextoOpcion")),
  reader.GetBoolean(reader.GetOrdinal("EsCorrecta"))
        );
}
