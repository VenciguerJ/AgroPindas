using Dapper;
using agropindas.Models;
using System.Data;
using System.ComponentModel;
using static Dapper.SqlMapper;


namespace agropindas.Repositories;
public class EstoqueRepository //aqui está a logica de tratamento no banco de lotes, suporte de calhas
{
    private readonly IDbConnection _dbConnection;

    public EstoqueRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Lote>> GetAll()
    {
        return await _dbConnection.QueryAsync<Lote>("SELECT * FROM Lote");
    }

    public async Task<IEnumerable<Lote>>GetAll(string str)
    {
        int Idproduto = int.Parse(str);
        return await _dbConnection.QueryAsync<Lote>("SELECT * FROM Lote WHERE IdProduto = @IdProduto", new { IdProduto = Idproduto });
    }

    public async Task<Lote?> Get(int id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Lote>("SELECT * FROM Lote WHERE IdProduto = @Id", new { Id = id });
    }

    public async Task <Lote?> Get(string str) 
    {
        // pega a ultima inserção no banco
        return await _dbConnection.QueryFirstOrDefaultAsync<Lote>("Select IDENT_CURRENT('@Tabela') AS ID;", new {Tabela = str });
    }

    public async Task Add(Lote entity)
    {
		var query = @"INSERT INTO Lote (IdCompra, IdProduto, QuantidadeLote, QuantidadeSaida) 
                    VALUES (@IdCompra, @IdProduto, @QuantidadeLote,0)";

        var resultado = await _dbConnection.ExecuteAsync(query, entity);

	}

    public async Task Update(Lote func)
    {
        var query = @"UPDATE Lote SET 
                    IdCompra = @IdCompra, 
                    IdProduto = @IdProduto,
                    QuantidadeLote = @QuantidadeLote ,
                    QuantidadeSaida = @QuantidadeSaida
                    WHERE IdProduto = @IdProduto;";

        await _dbConnection.ExecuteAsync(query, func);
    }

    public async Task Delete(int idProd, int IdCompra)
    {
        await _dbConnection.ExecuteAsync("DELETE FROM Lote WHERE IdProduto = @Id1 and IdCompra = @Id2", new { Id1 = idProd, Id2 = IdCompra });
    }


    //suporte de calhas
    public async Task AddSuporte(SuporteCalha sup)
    {
        string query = @"INSERT INTO Suporte_Calhas (CapacidadeMudas, ocupada) VALUES (@CapacidadeMudas, @ocupada)";
        await _dbConnection.ExecuteAsync(query, sup);
    }
    public async Task<IEnumerable<SuporteCalha>> GetAllCalhas()
    {
        return await _dbConnection.QueryAsync<SuporteCalha>("SELECT * FROM Suporte_Calhas");
    }

    public async Task<SuporteCalha> GetCalha(int Id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<SuporteCalha>("SELECT * FROM Suporte_Calhas WHERE id = @id", new { id = Id });
    }

    public async Task UpdateSuporte(SuporteCalha sup)
    {
        var query = @"UPDATE Suporte_Calhas SET 
                    CapacidadeMudas = @CapacidadeMudas, 
                    ocupada = @ocupada
                    WHERE Id = @Id";

        await _dbConnection.ExecuteAsync(query, sup);
    }


    public async Task DeleteSuporte(int id)
    {
        var query = @"
                        Delete From Suporte_Calhas where Id = @Id
                    ";
        var resultado = await _dbConnection.ExecuteAsync(query, new { Id = id });
    }

    //Fertilizante Calhas


    public async Task AddFertilizanteCalha(Fertilizante entity)
    {
        var query = @"INSERT INTO Suporte_Calhas_Fertilizantes (IdSuporte, IdProduto, QtdUtilizada) 
                    VALUES (@IdSuporte, @Id, @QtdUtilizada)";

        var resultado = await _dbConnection.ExecuteAsync(query, entity);

    }

    public async Task<IEnumerable<Fertilizante>> GetAllFertilizantes(int id)
    {
        var query = "SELECT IdProduto AS Id, IdSuporte, QtdUtilizada FROM Suporte_Calhas_Fertilizantes WHERE IdSuporte = @Id";

        return await _dbConnection.QueryAsync<Fertilizante>(query, new { Id = id });
    }

    public async Task DeleteFertilizante(int Id)
    {
        var query = "DELETE FROM Suporte_Calhas_Fertilizantes where IdSuporte = @id";

         await _dbConnection.ExecuteAsync(query, new { id = Id});
    }

    public async Task Colheita(LoteMuda entity)
    {
        var query = @"INSERT INTO Estoque_Produto
                        (id_estoque_produto, 
                        id_producao, 
                        quantidade_inicial, 
                        quantidade_vendido)
                    VALUES
                        (@IdEstoqueProduto,
                        @IdProducao,
                        @QuantidadeInicial,
                        0)";

        await _dbConnection.ExecuteAsync(query, entity);
    }

    public async Task<IEnumerable<LoteMuda>> GetAllMudas()
    {
        var query = @"
        SELECT 
            id AS Id,
            id_estoque_produto AS IdEstoqueProduto,
            id_producao AS IdProducao,
            quantidade_inicial AS QuantidadeInicial,
            quantidade_vendido AS QuantidadeVendido
        FROM Estoque_Produto";

        var resultado = await _dbConnection.QueryAsync<LoteMuda>(query);
        return resultado;
    }

    public async Task DeleteLoteMuda(int id)
    {
        await _dbConnection.ExecuteAsync("DELETE FROM Estoque_Produto WHERE id = @Id", new { Id = id } );
    }

    public async Task Perda(Lote func)
    {
        var query = @"UPDATE Lote SET 
                    IdCompra = @IdCompra, 
                    IdProduto = @IdProduto,
                    QuantidadeLote = @QuantidadeLote ,
                    QuantidadeSaida = @QuantidadeSaida
                    WHERE IdCompra = @IdCompra AND IdProduto = @IdProduto";

        await _dbConnection.ExecuteAsync(query, func);
    }


}