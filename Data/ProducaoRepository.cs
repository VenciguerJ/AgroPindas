using Dapper;
using agropindas.Models;
using System.Data;

namespace agropindas.Repositories;
public class ProducaoRepository : ICrudRepository<Producao>
{
    private readonly IDbConnection _dbConnection;

    public ProducaoRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Producao>> GetAll()
    {
        return await _dbConnection.QueryAsync<Producao>("SELECT * FROM Producao");
    }
    public async Task<IEnumerable<Producao>> GetAll(string id)
    {
        int realId = int.Parse(id);
        return await _dbConnection.QueryAsync<Producao>("SELECT * FROM Producao WHERE Id = @Id", new { Id = realId });
    }
    public async Task<Producao?> Get(int idAtivo)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Producao>("SELECT * FROM Producao WHERE IdCalha = @Id and finalizada = 1", new { Id = idAtivo });
    }

    public async Task <Producao?> Get(string IdInativo)
    {
        int realId = int.Parse(IdInativo);
        return await _dbConnection.QueryFirstOrDefaultAsync<Producao>("SELECT * FROM Producao WHERE IdCalha = @Id and finalizada = 0", new { Id = IdInativo });
    }

    public async Task Add(Producao entity)
    {
		var query = @"INSERT INTO Producao 
                    (IdLoteUsado, IdProdutoProduzido, QuantidadeProduzido, IdCalha, DiaColheita, finalizada) 
                    VALUES (@IdLoteUsado, @IdProdutoProduzido, @QuantidadeProduzido, @IdCalha, @DiaColheita, @Finalizada)";

        var resultado = await _dbConnection.ExecuteAsync(query, entity);
        Console.WriteLine(resultado);
	}

    public async Task Update(Producao func)
    {
        var query = @"UPDATE Producao SET 
                        IdLoteUsado = @IdLoteUsado, 
                        IdProdutoProduzido = @IdProdutoProduzido, 
                        QuantidadeProduzido = @QuantidadeProduzido, 
                        IdCalha = @IdCalha, 
                        DiaColheita = @DiaColheita, 
                        finalizada = @Finalizada
                        Where Id = @Id";
        await _dbConnection.ExecuteAsync(query, func);
    }

    public async Task Delete(int id)
    {
        await _dbConnection.ExecuteAsync("DELETE FROM Producao WHERE Id = @Id", new { Id = id });
    }
}
