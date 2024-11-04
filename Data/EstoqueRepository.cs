﻿using Dapper;
using agropindas.Models;
using System.Data;
using System.ComponentModel;


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
        int IdProduto = int.Parse(str);
        return await _dbConnection.QueryAsync<Lote>("SELECT * FROM Lote where IdProduto = @Nome", new {Nome = IdProduto });
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
}