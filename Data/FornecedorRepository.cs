using Dapper;
using agropindas.Models;
using System.Data;

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
    public async Task<IEnumerable<Fornecedor>> GetAll(string nome)
    {
        return await _dbConnection.QueryAsync<Fornecedor>("SELECT * FROM Fornecedor  WHERE RazaoSocial LIKE @Nome OR CNPJ LIKE @Nome",new { Nome = "%" + nome + "%" });
    }
    public async Task<Fornecedor?> Get(int num)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Fornecedor>("SELECT * FROM Fornecedor WHERE Id = @Id", new { Id = num });
    }

    public async Task <Fornecedor?> Get(string cnpj)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Fornecedor>("SELECT * FROM Fornecedor WHERE CNPJ = @CNPJ", new { CNPJ = cnpj });
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
        func.Debugger();
        var query = @"UPDATE Fornecedor SET 
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
        await _dbConnection.ExecuteAsync("DELETE FROM Fornecedor WHERE Id = @Id", new { Id = id });
    }
}
