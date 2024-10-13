using Dapper;
using agropindas.Models;
using System.Data;


namespace agropindas.Repositories;
public class FuncionarioRepository : ICrudRepository<Funcionario>
{
    private readonly IDbConnection _dbConnection;

    public FuncionarioRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Funcionario>> GetAll()
    {
        return await _dbConnection.QueryAsync<Funcionario>("SELECT * FROM Funncionarios");
    }

    public async Task<IEnumerable<Funcionario>> GetAll(string nome)
    {
        return await _dbConnection.QueryAsync<Funcionario>("SELECT * FROM Funncionarios where Nome Like @Nome", new { Nome = nome });
    }

    public async Task<Funcionario?> Get(int id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Funcionario>("SELECT * FROM Ingredientes WHERE Id = @Id", new { Id = id });
    }

    public async Task <Funcionario?> Get(string email)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Funcionario>("SELECT * FROM Funcionarios WHERE email = @Email", new { Email = email });
    }

    public async Task Add(Funcionario entity)
    {
		var query = @"INSERT INTO Funcionarios (cpf, senha, nome, telefone, email, id_cargo, data_nascimento) 
                    VALUES (@cpf, @senha, @nome, @telefone, @email,@id_cargo, @data_nascimento)";

		var parameters = new Dictionary<string, object>
        {
            { "cpf", entity.CPF },
            { "senha", entity.Senha },
            { "nome", entity.Nome },
            { "telefone", entity.Fone },
            {"email", entity.Email },
            { "id_cargo", entity.IdCargo },
            { "data_nascimento", entity.DataNascimento }
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

    public async Task Update(Funcionario func)
    {
        var query = "UPDATE Ingredientes SET Nome = @Nome, UnidadeMedida = @UnidadeMedida WHERE Id = @Id";
        await _dbConnection.ExecuteAsync(query, func);
    }

    public async Task Delete(int id)
    {
        await _dbConnection.ExecuteAsync("DELETE FROM Ingredientes WHERE Id = @Id", new { Id = id });
    }
}
