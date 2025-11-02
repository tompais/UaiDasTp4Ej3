using ABS;
using CONTEXT;
using Microsoft.Extensions.DependencyInjection;
using REPO;
using SERV;

namespace APP;

public static class Configuracion
{
    private static ServiceProvider? _serviceProvider;

    public static void ConfigurarServicios()
    {
        var services = new ServiceCollection();

        // Configurar connection string
        var connectionString = ObtenerConnectionString();
     
        // Registrar DatabaseContext como Singleton
        services.AddSingleton(new DatabaseContext(connectionString));

        // Registrar Repositorios como Scoped (una instancia por scope/request)
        services.AddScoped<IRepositorioCategoria, RepositorioCategoria>();
        services.AddScoped<IRepositorioPregunta, RepositorioPregunta>();
        services.AddScoped<IRepositorioOpcionRespuesta, RepositorioOpcionRespuesta>();
        services.AddScoped<IRepositorioJugador, RepositorioJugador>();
        services.AddScoped<IRepositorioPartidaJuego, RepositorioPartidaJuego>();
        services.AddScoped<IRepositorioRespuestaJugador, RepositorioRespuestaJugador>();

        // Registrar Servicios como Scoped
        services.AddScoped<ServicioJuego>();

        _serviceProvider = services.BuildServiceProvider();
    }

    public static IServiceProvider ServiceProvider
    {
        get
        {
            if (_serviceProvider == null)
            {
                ConfigurarServicios();
            }
            return _serviceProvider!;
        }
    }

    public static T ObtenerServicio<T>() where T : notnull => ServiceProvider.GetRequiredService<T>();

    private static string ObtenerConnectionString()
    {
        // Intentar obtener desde variables de entorno individuales
        var server = Environment.GetEnvironmentVariable("TRIVIA_DB_SERVER");
        var database = Environment.GetEnvironmentVariable("TRIVIA_DB_DATABASE");
        var userId = Environment.GetEnvironmentVariable("TRIVIA_DB_USER");
        var password = Environment.GetEnvironmentVariable("TRIVIA_DB_PASSWORD");

        // Si están todas las variables configuradas, usar SQL Server Authentication
        if (!string.IsNullOrEmpty(server) &&
            !string.IsNullOrEmpty(database) &&
            !string.IsNullOrEmpty(userId) &&
            !string.IsNullOrEmpty(password))
        {
            return $"Server={server};Database={database};User Id={userId};Password={password};TrustServerCertificate=true;";
        }

        // Intentar obtener connection string completo desde variable de entorno
        var connectionStringEnv = Environment.GetEnvironmentVariable("TRIVIA_CONNECTION_STRING");
        if (!string.IsNullOrEmpty(connectionStringEnv))
        {
            return connectionStringEnv;
        }

        // Valor por defecto: Windows Authentication en localhost
        return "Server=localhost;Database=TriviaDB;Integrated Security=true;TrustServerCertificate=true;";
    }
}
