namespace ABS;

public interface IRepositorioBase<T> where T : class
{
    void Agregar(T entidad);
    void Modificar(T entidad);
    void Eliminar(int id);
    T? ObtenerPorId(int id);
    List<T> ObtenerTodos();
}
