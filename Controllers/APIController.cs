using Microsoft.AspNetCore.Mvc;
using agropindas.Repositories;
using agropindas.Models;
using System.Diagnostics.Contracts;
using Microsoft.EntityFrameworkCore.Query;

namespace agropindas.Controllers;

[ApiController] // Necessário para APIs
[Route("[controller]")]
public class APIController : ControllerBase
{
    private readonly EstoqueRepository _estoque;
    private readonly ICrudRepository<Produto> _produtos;
    private readonly ICrudRepository<Cliente> _cliente;
    
    public APIController(EstoqueRepository e, ICrudRepository<Produto> p, ICrudRepository<Cliente> c)
    {
        _estoque = e;
        _produtos = p;
        _cliente = c;
    }

    //AJAX FUNCTION
    [HttpGet("GetLotesByProduto/{produtoId}")]
    public async Task<IActionResult> GetLotesByProduto(int produtoId)
    {
        try
        {
            var lotes = await _estoque.GetAll(produtoId.ToString());
            return Ok(lotes);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"-----\n{ex.ToString}\n------------");
            return StatusCode(500, "Erro interno ao buscar lotes.");
        }
    }

    //AJAX FUNCTION
    [HttpGet("GetProduto/{produtoId}")]
    public async Task<IActionResult> GetProduto(int produtoId)
    {
        try
        {
            var prod = await _produtos.Get(produtoId);
            if (prod == null)
            {
                return NotFound("Produto não encontrado.");
            }
            return Ok(new { preco = prod.ValorProduto });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"-----\n{ex.ToString}\n------------");
            return StatusCode(500, "Erro interno ao buscar produto.");
        }
    }


    //------------------funcções de funcionário------------------------

    [HttpPost("AddClient")]
    public async Task<IActionResult> AddClient([FromBody] Cliente cliente)
    {
        if (cliente == null)
        {
            return BadRequest("JSON inválido.");
        }

        try
        {
            Cliente TmpClient = new Cliente();
            TmpClient = cliente;
            await _cliente.Add(TmpClient);
            return Ok(new { mensagem = "Cliente recebido com sucesso!", dados = cliente });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("Authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] Cliente cliente)
    {
        // Verifica se o objeto cliente foi enviado corretamente
        if (cliente == null || string.IsNullOrEmpty(cliente.CPF) || string.IsNullOrEmpty(cliente.Senha))
        {
            return BadRequest("JSON inválido ou dados incompletos. Certifique-se de fornecer o email e a senha.");
        }

        try
        {
            // Busca o cliente no repositório pelo email
            var tmpCliente = await _cliente.Get(cliente.CPF);

            // Verifica se o cliente foi encontrado
            if (tmpCliente == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Valida a senha
            if (cliente.Senha != tmpCliente.Senha)
            {
                return Unauthorized("Senha incorreta.");
            }

            return Ok(new
            {
                mensagem = "Usuário autenticado com sucesso.",
                dados = tmpCliente
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

}
