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
    private readonly ICrudRepository<PedidoDto> _pedidos;

    public APIController(EstoqueRepository e, ICrudRepository<Produto> p, ICrudRepository<Cliente> c, ICrudRepository<PedidoDto> pedidos)
    {
        _estoque = e;
        _produtos = p;
        _cliente = c;
        _pedidos = pedidos;
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

    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _produtos.GetAll();

        var filteredProducts = products.Where(p => p.TipoProduto == 2);

        return Ok(filteredProducts);
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
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateDto dtoc)
    {
        // Verifica se o objeto cliente foi enviado corretamente
        if (dtoc == null || string.IsNullOrEmpty(dtoc.CPF) || string.IsNullOrEmpty(dtoc.Senha))
        {
            return BadRequest("JSON inválido ou dados incompletos. Certifique-se de fornecer o email e a senha.");
        }

        try
        {
            // Busca o cliente no repositório pelo email
            var tmpCliente = await _cliente.Get(dtoc.CPF);

            // Verifica se o cliente foi encontrado
            if (tmpCliente == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Valida a senha
            if (dtoc.Senha != tmpCliente.Senha)
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

    [HttpPost("pedido")]
    public async Task<IActionResult> CriarPedido([FromBody] PedidoDto pedidoRequest)
    {
        // Validação básica
        if (pedidoRequest == null || string.IsNullOrEmpty(pedidoRequest.CPF) || string.IsNullOrEmpty(pedidoRequest.Produtos))
        {
            return BadRequest("Os campos CPF e Produtos são obrigatórios.");
        }

        try
        {
            PedidoDto dto = new PedidoDto();

            dto.Produtos = pedidoRequest.Produtos;
            dto.CPF = pedidoRequest.CPF;

            // Se tiver um repositório para salvar o pedido
            await _pedidos.Add(dto);

            return Ok(new { mensagem = "Pedido criado com sucesso!", dados = dto });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao processar o pedido: {ex.Message}");
        }
    }

    [HttpGet("GetPedidosByCpf/{cpf}")]
    public async Task<IActionResult> GetPedidosByCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
        {
            return BadRequest("CPF não pode ser vazio.");
        }

        if (cpf.Length != 11 || !cpf.All(char.IsDigit))
        {
            return BadRequest("CPF inválido. Deve conter 11 dígitos numéricos.");
        }

        var pedidos = await _pedidos.GetAll(cpf);

        if (pedidos == null || !pedidos.Any())
        {
            return NotFound("Nenhum pedido encontrado para este CPF.");
        }

        return Ok(pedidos);
    }
}
