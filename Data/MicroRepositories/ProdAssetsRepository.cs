using Dapper;
using agropindas.Models;
using System.Data;

namespace agropindas.Repositories;

public class ProdAssetsRepository : ISelectItems<ProdAssets>
{
    private readonly IDbConnection _dbConnection;
    public ProdAssetsRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<ProdAssets>> GetAll(string tabela)
    {
        if (string.IsNullOrEmpty(tabela))
            throw new ArgumentException("Nome da tabela não pode ser nulo ou vazio", nameof(tabela));

        var query = $"SELECT * FROM {tabela}";

        var result = await _dbConnection.QueryAsync<ProdAssets>(query);

        await Console.Out.WriteLineAsync(result.ToString());

        return result;
    }

    public async Task<ProdAssets> Get(string table, int id)
    {
        if (string.IsNullOrEmpty(table))
            throw new ArgumentException("Nome da tabela não pode ser nulo ou vazio", nameof(table));

        var query = $"SELECT * FROM {table} WHERE Id = @Id";

        return await _dbConnection.QueryFirstOrDefaultAsync<ProdAssets>(query, new { Id = id });
    }

    public async Task<ProdAssets> Get(string table, string nome)
    {
        if (string.IsNullOrEmpty(table))
            throw new ArgumentException("Nome da tabela não pode ser nulo ou vazio", nameof(table));

        string query = $"SELECT * FROM {table} WHERE email = @Nome";
        return await _dbConnection.QueryFirstOrDefaultAsync<ProdAssets>(query, new { Nome = nome, Tabela = table});
    }
}

