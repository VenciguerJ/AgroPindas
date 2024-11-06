﻿using Dapper;
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
    public async Task<IEnumerable<Producao>> GetAll(string nome)
    {
        return await _dbConnection.QueryAsync<Producao>("SELECT * FROM Producao  WHERE RazaoSocial LIKE @Nome OR CNPJ LIKE @Nome",new { Nome = "%" + nome + "%" });
    }
    public async Task<Producao?> Get(int num)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Producao>("SELECT * FROM Producao WHERE Id = @Id", new { Id = num });
    }

    public async Task <Producao?> Get(string cnpj)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Producao>("SELECT * FROM Producao WHERE CNPJ = @CNPJ", new { CNPJ = cnpj });
    }

    public async Task Add(Producao entity)
    {
		var query = @"INSERT INTO Producao 
                    (Id, IdLoteUsado, IdProdutoProduzido, QuantidadeProduzido, IdCalha) 
                    VALUES (@Id, @IdLoteUsado, @IdProdutoProduzido, @QuantidadeProduzido, @IdCalha)";

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

    public async Task Update(Producao func)
    {
        var query = @"UPDATE Producao SET 
                        CNPJ = @CNPJ, 
                        RazaoSocial = @RazaoSocial, 
                        Endereco = @Endereco, 
                        Fone = @Fone, 
                        Email = @Email 
                    WHERE Id = @Id";
        await _dbConnection.ExecuteAsync(query, func);
    }

    public async Task Delete(int id)
    {
        await _dbConnection.ExecuteAsync("DELETE FROM Producao WHERE Id = @Id", new { Id = id });
    }
}
