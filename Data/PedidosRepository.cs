using Dapper;
using agropindas.Models;
using System.Data;
using System.ComponentModel;


namespace agropindas.Repositories;
public class PedidoRepository : ICrudRepository<PedidoDto>
{
    private readonly IDbConnection _dbConnection;

    public PedidoRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<PedidoDto>> GetAll()
    {
        return await _dbConnection.QueryAsync<PedidoDto>("SELECT * FROM Pedidos");
    }

    public async Task<IEnumerable<PedidoDto>>GetAll(string cpf)
    {
        return await _dbConnection.QueryAsync<PedidoDto>("SELECT * FROM Pedidos where CPF = @CPF", new {CPF = cpf});
    }

    public async Task<PedidoDto?> Get(int id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<PedidoDto>("SELECT * FROM Pedidos WHERE Id = @Id", new { Id = id });
    }

    public async Task <PedidoDto?> Get(string nome)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<PedidoDto>("SELECT * FROM Pedidos where Nome = @Nome", new { Nome = nome });
    }

    public async Task Add(PedidoDto entity)
    {
        var query = @"INSERT INTO Pedidos (CPF, Produtos, DataPedido) 
                    VALUES (@CPF, @Produtos, getdate())";

        await _dbConnection.ExecuteAsync(query, entity);
    }

    public async Task Update(PedidoDto func)
    {
        var query = @"UPDATE Pedidos SET 
                    Nome = @Nome, 
                    Descricao = @Descricao, 
                    TemperaturaPlantio = @TemperaturaPlantio ,
                    DiasColheita = @DiasColheita,
                    UnidadeCadastro = @UnidadeCadastro,
                    TipoProduto = @TipoProduto,
                    ValorProduto = @ValorProduto
                    WHERE Id = @Id";

        await _dbConnection.ExecuteAsync(query, func);
    }

    public async Task Delete(int id)
    {
        await _dbConnection.ExecuteAsync("DELETE FROM Pedidos WHERE Id = @Id", new { Id = id });
    }
}
