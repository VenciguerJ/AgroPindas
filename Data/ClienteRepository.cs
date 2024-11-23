using Dapper;
using agropindas.Models;
using System.Data;

namespace agropindas.Repositories;
public class ClienteRepository : ICrudRepository<Cliente>
{
    private readonly IDbConnection _dbConnection;

    public ClienteRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Cliente>> GetAll()
    {
        return await _dbConnection.QueryAsync<Cliente>("SELECT * FROM Clientes");
    }
    public async Task<IEnumerable<Cliente>> GetAll(string cpf)
    {
        return await _dbConnection.QueryAsync<Cliente>("SELECT * FROM Clientes WHERE CPF = @CPF",new { CPF = cpf });
    }
    public async Task<Cliente?> Get(int num)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Cliente>("SELECT * FROM Cliente WHERE Id = @Id", new { Id = num });
    }

    public async Task <Cliente?> Get(string cpf)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Cliente>("SELECT * FROM Clientes WHERE CPF = @CPF", new { CPF = cpf });
    }

    public async Task Add(Cliente entity)
    {
		var query = @"INSERT INTO Clientes (Nome, Email, CPF, Senha) 
                    VALUES (@Nome, @Email, @CPF, @Senha)";

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

    public async Task Update(Cliente func)
    {
        var query = @"UPDATE Cliente SET 
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
        await _dbConnection.ExecuteAsync("DELETE FROM Clientes WHERE Id = @Id", new { Id = id });
    }
}
