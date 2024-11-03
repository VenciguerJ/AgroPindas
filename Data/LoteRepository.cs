using Dapper;
using agropindas.Models;
using System.Data;
using System.ComponentModel;


namespace agropindas.Repositories;
public class LoteRepository : ICrudRepository<Lote>
{
    private readonly IDbConnection _dbConnection;

    public LoteRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Lote>> GetAll()
    {
        return await _dbConnection.QueryAsync<Lote>("SELECT * FROM Lote");
    }

    public async Task<IEnumerable<Lote>>GetAll(string nome)
    {
        return await _dbConnection.QueryAsync<Lote>("SELECT * FROM Lote where Nome LIKE @Nome", new {Nome = nome});
    }

    public async Task<Lote?> Get(int id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Lote>("SELECT * FROM Lote WHERE Id = @Id", new { Id = id });
    }

    public async Task <Lote?> Get(string nome)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Lote>("SELECT * FROM Lote == @Nome", new { Nome = nome });
    }

    public async Task Add(Lote entity)
    {
		var query = @"INSERT INTO Lote (IdProduto, QuantidadeLote QuantidadeSaida) 
                    VALUES (@IdProduto, @QuantidadeLote, @QuantidadeSaida)";

        var resultado = await _dbConnection.ExecuteAsync(query, entity);
        Console.WriteLine(resultado);

	}

    public async Task Update(Lote func)
    {
        var query = @"UPDATE Lote SET 
                    Nome = @Nome, 
                    Descricao = @Descricao, 
                    TemperaturaPlantio = @TemperaturaPlantio ,
                    DiasColheita = @DiasColheita,
                    UnidadeCadastro = @UnidadeCadastro,
                    TipoLote = @TipoLote
                    WHERE Id = @Id";

        await _dbConnection.ExecuteAsync(query, func);
    }

    public async Task Delete(int id)
    {
        await _dbConnection.ExecuteAsync("DELETE FROM Lote WHERE Id = @Id", new { Id = id });
    }
}
