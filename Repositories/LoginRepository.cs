using System.Data;
using Dapper;
using agropindas.Models;
using Microsoft.Data.SqlClient;
using static Dapper.SqlMapper;

namespace agropindas.Repositories;
public class LoginRepository : ILogin<Funcionario>
{
    private readonly IDbConnection _dbConnection;
    public LoginRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public async Task<Funcionario> GetFunc(Funcionario entity)
    {
        var query = @"select *
        from Funcionarios
        where email = @email and senha = @senha";

        var parameters = new Dictionary<string, object>
        {
            { "id_usuario", entity.Id},
            { "cpf", entity.CPF },
            { "senha", entity.Senha },
            { "nome", entity.Nome },
            { "telefone", entity.Fone },
            { "email", entity.Email },
            { "id_cargo", entity.IdCargo },
            { "superior", entity.Superior },
            { "data_nascimento", entity.DataNascimento }
        };
        return await _dbConnection.QueryFirstOrDefaultAsync<Funcionario>(query, parameters);
    }
    public async Task<Funcionario?> VerificaUserExistente(Funcionario entity)
    {
        var query = @"select * from Funcionarios Where 
                    cpf = @cpf or
                    nome = @nome or
                    telefone = @telefone or
                    email = @email;";

        var parameters = new Dictionary<string, object>
        {
            { "id_usuario", entity.Id},
            { "cpf", entity.CPF },
            { "senha", entity.Senha },
            { "nome", entity.Nome },
            { "telefone", entity.Fone },
            { "email", entity.Email },
            { "id_cargo", entity.IdCargo },
            { "superior", entity.Superior },
            { "data_nascimento", entity.DataNascimento }
        };
        return await _dbConnection.QueryFirstOrDefaultAsync<Funcionario>(query, parameters);
    }
}

