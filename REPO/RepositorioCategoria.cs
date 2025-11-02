using System.Data;
using Microsoft.Data.SqlClient;
using ABS;
using CONTEXT;
using DOM;

namespace REPO;

public class RepositorioCategoria(DatabaseContext context) : IRepositorioCategoria
{
    private readonly DatabaseContext _context = context;

public void Agregar(Categoria entidad)
  {
   using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_AgregarCategoria", conexion)
    {
     CommandType = CommandType.StoredProcedure
        };
        
        comando.Parameters.AddWithValue("@Nombre", entidad.Nombre);
        comando.Parameters.AddWithValue("@Descripcion", entidad.Descripcion);
        
        conexion.Open();
        comando.ExecuteNonQuery();
    }

    public void Modificar(Categoria entidad)
    {
        using var conexion = _context.CrearConexion();
      using var comando = new SqlCommand("sp_ModificarCategoria", conexion)
      {
 CommandType = CommandType.StoredProcedure
        };
      
        comando.Parameters.AddWithValue("@Id", entidad.Id);
        comando.Parameters.AddWithValue("@Nombre", entidad.Nombre);
        comando.Parameters.AddWithValue("@Descripcion", entidad.Descripcion);
        
  conexion.Open();
      comando.ExecuteNonQuery();
    }

    public void Eliminar(int id)
    {
        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_EliminarCategoria", conexion)
        {
 CommandType = CommandType.StoredProcedure
        };
        
        comando.Parameters.AddWithValue("@Id", id);
        
        conexion.Open();
    comando.ExecuteNonQuery();
  }

    public Categoria? ObtenerPorId(int id)
    {
        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_ObtenerCategoriaPorId", conexion)
      {
            CommandType = CommandType.StoredProcedure
    };
        
        comando.Parameters.AddWithValue("@Id", id);
        
   conexion.Open();
        using var reader = comando.ExecuteReader();
        
    if (reader.Read())
        {
        return MapearCategoria(reader);
    }
        
     return null;
    }

    public List<Categoria> ObtenerTodos()
    {
        var categorias = new List<Categoria>();
      
        using var conexion = _context.CrearConexion();
        using var comando = new SqlCommand("sp_ObtenerTodasCategorias", conexion)
        {
    CommandType = CommandType.StoredProcedure
        };
        
        conexion.Open();
        using var reader = comando.ExecuteReader();
        
        while (reader.Read())
        {
    categorias.Add(MapearCategoria(reader));
        }
        
        return categorias;
    }

    private static Categoria MapearCategoria(SqlDataReader reader) => 
     new(
       reader.GetInt32(reader.GetOrdinal("Id")),
    reader.GetString(reader.GetOrdinal("Nombre")),
        reader.GetString(reader.GetOrdinal("Descripcion"))
        );
}
