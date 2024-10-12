using Dapper;
using agropindas.Models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace agropindas.Repositories;
public class UnidadeCadastroRepository : ICrudRepository<UnidadeCadastro>
{
    private readonly IDbConnection _dbConnection;

    public UnidadeCadastroRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<UnidadeCadastro>> GetAll()
    {
        return await _dbConnection.QueryAsync<UnidadeCadastro>("SELECT * FROM UnidadeCadastro");
    }

    public async Task<UnidadeCadastro?> Get(int id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<UnidadeCadastro>("SELECT * FROM Produto WHERE Id = @Id", new { Id = id });
    }

    public async Task <UnidadeCadastro?> Get(string Nome)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<UnidadeCadastro>("SELECT * FROM Produtos WHERE email = @Email", new { Email = Nome });
    }

    public async Task Add(UnidadeCadastro entity)
    {
        Console.WriteLine("Tentou passar pelo banco de dados");
		var query = @"INSERT INTO Produto (Id, Nome, Descricao, TemperaturaPlantio, DiasColheita, UnidadeCadastro, TipoProduto) 
                    VALUES (@Id, @Nome, @Descricao, @TemperaturaPlantio, @DiasColheita, @UnidadeCadastro, @TipoProduto)";

        try
        {
            var resultado = await _dbConnection.ExecuteAsync(query, entity);
            Console.WriteLine(resultado);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
	}

    public async Task Update(UnidadeCadastro func)
    {
        var query = "UPDATE Ingredientes SET Nome = @Nome, UnidadeMedida = @UnidadeMedida WHERE Id = @Id";
        await _dbConnection.ExecuteAsync(query, func);
    }

    public async Task Delete(int id)
    {
        await _dbConnection.ExecuteAsync("DELETE FROM Ingredientes WHERE Id = @Id", new { Id = id });
    }
}
