using Dapper;
using agropindas.Models;
using System.Data;
using System.ComponentModel;


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

    public async Task<IEnumerable<Produto>>GetAll(string nome)
    {
        return await _dbConnection.QueryAsync<Produto>("SELECT * FROM Produto where Nome LIKE @Nome", new {Nome = nome});
    }

    public async Task<Produto?> Get(int id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Produto>("SELECT * FROM Produto WHERE Id = @Id", new { Id = id });
    }

    public async Task <Produto?> Get(string nome)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Produto>("SELECT * FROM Produto where Nome = @Nome", new { Nome = nome });
    }

    public async Task Add(Produto entity)
    {
        Console.WriteLine("Tentou passar pelo banco de dados");
		var query = @"INSERT INTO Produto (Nome, Descricao, TemperaturaPlantio, DiasColheita, UnidadeCadastro, TipoProduto, ValorProduto) 
                    VALUES (@Nome, @Descricao, @TemperaturaPlantio, @DiasColheita, @UnidadeCadastro, @TipoProduto, @ValorProduto)";
        
        int tipoprod = entity.TipoProduto;
        int unidadecadastro = entity.UnidadeCadastro;


        var parameters = new Dictionary<string, object>
        {
            { "Nome", entity.Nome },
            { "Descricao", entity.Descricao },
            { "TemperaturaPlantio", entity.TemperaturaPlantio },
            { "DiasColheita", entity.DiasColheita },
            { "UnidadeCadastro", unidadecadastro },
            { "TipoProduto", tipoprod},
            {"ValorProduto", entity.ValorProduto }
        };

        var resultado = await _dbConnection.ExecuteAsync(query, parameters);
        Console.WriteLine(resultado);

	}

    public async Task Update(Produto func)
    {
        var query = @"UPDATE Produto SET 
                    Nome = @Nome, 
                    Descricao = @Descricao, 
                    TemperaturaPlantio = @TemperaturaPlantio ,
                    DiasColheita = @DiasColheita,
                    UnidadeCadastro = @UnidadeCadastro,
                    TipoProduto = @TipoProduto
                    WHERE Id = @Id";

        await _dbConnection.ExecuteAsync(query, func);
    }

    public async Task Delete(int id)
    {
        await _dbConnection.ExecuteAsync("DELETE FROM Produto WHERE Id = @Id", new { Id = id });
    }
}
