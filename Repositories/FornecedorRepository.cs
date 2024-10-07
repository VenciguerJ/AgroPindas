using Dapper;
using agropindas.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using agropindas.Repositories;




namespace agropindas.Repositories;
public class FornecedorRepository : ICrudRepository<Fornecedor>
{
    private readonly IDbConnection _dbConnection;

    public FornecedorRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Fornecedor>> GetAll()
    {
        return await _dbConnection.QueryAsync<Fornecedor>("SELECT * FROM Fornecedor");
    }

    public async Task<Fornecedor?> Get(int num)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Fornecedor>("SELECT * FROM Ingredientes WHERE Id = @Id", new { Id = num });
    }

    public async Task <Fornecedor?> Get(string str)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Fornecedor>("SELECT * FROM Funcionarios WHERE email = @Email", new { Email = str });
    }

    public async Task Add(Fornecedor entity)
    {
        Console.WriteLine("Tentou passar pelo banco de dados");
		var query = @"INSERT INTO Fornecedor (CNPJ, RazaoSocial, Endereco, Fone, Email) 
                    VALUES (@CNPJ, @RazaoSocial, @Endereco, @Fone, @Email)";

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

    public async Task Update(Fornecedor func)
    {
        var query = "UPDATE Ingredientes SET Nome = @Nome, UnidadeMedida = @UnidadeMedida WHERE Id = @Id";
        await _dbConnection.ExecuteAsync(query, func);
    }

    public async Task Delete(int id)
    {
        await _dbConnection.ExecuteAsync("DELETE FROM Ingredientes WHERE Id = @Id", new { Id = id });
    }
}
