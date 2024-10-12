using Dapper;
using agropindas.Models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace agropindas.Repositories;
public class ProdutoRepository : ICrudRepository<Produto>
{
    private readonly IDbConnection _dbConnection;

    public ProdutoRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Produto>> GetAll()
    {
        return await _dbConnection.QueryAsync<Produto>("SELECT * FROM Produto");
    }

    public async Task<Produto?> Get(int id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Produto>("SELECT * FROM Produto WHERE Id = @Id", new { Id = id });
    }

    public async Task <Produto?> Get(string email)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Produto>("SELECT * FROM Produtos WHERE email = @Email", new { Email = email });
    }

    public async Task Add(Produto entity)
    {
        Console.WriteLine("Tentou passar pelo banco de dados");
		var query = @"INSERT INTO Produto (Id, Nome, Descricao, TemperaturaPlantio, DiasColheita, UnidadeCadastro, TipoProduto) 
                    VALUES (@Id, @Nome, @Descricao, @TemperaturaPlantio, @DiasColheita, @UnidadeCadastro, @TipoProduto)";
        var parameters = new Dictionary<string, object>
        {
            { "Id", entity.Id },
            { "Nome", entity.Nome },
            { "Descricao", entity.Descricao },
            { "TemperaturaPlantio", entity.TemperaturaPlantio },
            { "DiasColheita", entity.DiasColheita },
            { "UnidadeCadastro", entity.UnidadeCadastro.Id }, // Certifique-se de passar o Id da UnidadeCadastro
            { "TipoProduto", entity.TipoProduto }
        };
        try
        {
            var resultado = await _dbConnection.ExecuteAsync(query, parameters);
            Console.WriteLine(resultado);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
	}

    public async Task Update(Produto func)
    {
        var query = "UPDATE Ingredientes SET Nome = @Nome, UnidadeMedida = @UnidadeMedida WHERE Id = @Id";
        await _dbConnection.ExecuteAsync(query, func);
    }

    public async Task Delete(int id)
    {
        await _dbConnection.ExecuteAsync("DELETE FROM Ingredientes WHERE Id = @Id", new { Id = id });
    }
}
