namespace agropindas.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICrudRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetAll(string str);
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

public interface ISelectItems<T>
{
    Task<IEnumerable<T>> GetAll(string Table);
    Task<T?> Get(string table, int num);
    Task<T?> Get(string table, string str);
}