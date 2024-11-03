using Dapper;
using agropindas.Models;
using System.Data;
using System.ComponentModel;


namespace agropindas.Repositories;
public class CompraRepositoy : ICrudRepository<Compra>
{
    private readonly IDbConnection _dbConnection;

    public CompraRepositoy(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Compra>> GetAll()
    {
        return await _dbConnection.QueryAsync<Compra>("SELECT * FROM Compra");
    }

    public async Task<IEnumerable<Compra>>GetAll(string nome)
    {
        return await _dbConnection.QueryAsync<Compra>("SELECT * FROM Compra where Nome LIKE @Nome", new {Nome = nome});
    }

    public async Task<Compra?> Get(int id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Compra>("SELECT * FROM Compra WHERE Id = @Id", new { Id = id });
    }

    public async Task <Compra?> Get(string nome)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Compra>("SELECT * FROM Compra == @Nome", new { Nome = nome });
    }

    public async Task Add(Compra entity)
    {
		var query = @"INSERT INTO Compra (IdFornecedor, DataCompra, ValorTotal) 
                    VALUES (@IdFornecedor, @DataCompra, @ValorTotal)";

        var resultado = await _dbConnection.ExecuteAsync(query, entity);
        Console.WriteLine(resultado);

	}

    public async Task Update(Compra func)
    {
        var query = @"UPDATE Compra SET 
                    Nome = @Nome, 
                    Descricao = @Descricao, 
                    TemperaturaPlantio = @TemperaturaPlantio ,
                    DiasColheita = @DiasColheita,
                    UnidadeCadastro = @UnidadeCadastro,
                    TipoCompra = @TipoCompra
                    WHERE Id = @Id";

        await _dbConnection.ExecuteAsync(query, func);
    }

    public async Task Delete(int id)
    {
        await _dbConnection.ExecuteAsync("DELETE FROM Compra WHERE Id = @Id", new { Id = id });
    }
}
