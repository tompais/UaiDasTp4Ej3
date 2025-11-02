using Microsoft.Data.SqlClient;

namespace CONTEXT;

public class DatabaseContext(string connectionString)
{
    private readonly string _connectionString = connectionString;

    public SqlConnection CrearConexion() => new(_connectionString);
}
