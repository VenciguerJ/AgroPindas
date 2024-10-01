namespace agropindas.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICrudRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> Get(int id);
    Task<T?> Get(string str);
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(int id);
}
public interface ILogin<T>
{
    Task<T?> GetFunc(T entity);
    Task<T?> VerificaUserExistente(T entity);
}