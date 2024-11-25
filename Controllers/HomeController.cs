using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using agropindas.Models;
using agropindas.Repositories;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;
using agropindas.Models.ViewModels;

namespace agropindas.Controllers;

public class HomeController : Controller
{
    private readonly ILogin<Funcionario> _login;
    private readonly ICrudRepository<Funcionario> _crudRepository;
    private readonly ICrudRepository<Cliente > _clienteRepository;
    private readonly ICrudRepository<Produto> _produtoRepository;
    private readonly ICrudRepository<Producao> _producaoRepository;
    private readonly ICrudRepository<PedidoDto> _pedidosRepository;

    public HomeController(ICrudRepository<Funcionario> FuncionarioRepository, ILogin<Funcionario> Ilogin, ICrudRepository<Cliente> clienteRepository, ICrudRepository<Produto> produtoRepository, ICrudRepository<Producao> producaoRepository, ICrudRepository<PedidoDto> ped)
    {
        _login = Ilogin;
        _crudRepository = FuncionarioRepository;
        _clienteRepository = clienteRepository;
        _produtoRepository = produtoRepository;
        _producaoRepository = producaoRepository;
        _pedidosRepository = ped;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(Funcionario formFunc)
    {

        var funcionarioDB = await _login.GetFunc(formFunc);
        
        if (funcionarioDB != null)
        {
            if (formFunc.Senha == funcionarioDB.Senha)
            {
                return RedirectToAction("HomePage");
            }
            else
            {
                TempData["ErrorMessage"] = "Senha Incorreta!";
                return RedirectToAction("Index");
            }
        }
        else
        {
            TempData["ErrorMessage"] = "Usuário não encontrado!";
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(Funcionario formFunc)
    {
        
		if (!ModelState.IsValid)
        {
            
            return View(formFunc);
        }
        try
        {

            Funcionario jaExiste = await _login.VerificaUserExistente(formFunc);

            if(jaExiste == null)
            {
                Console.WriteLine("veio");
                await _crudRepository.Add(formFunc);
                TempData["SuccessMessage"] = "Usuário criado com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Já existe um usuário com estas informações";
            }
        }
        catch (SqlException sqlEx)
        {
            TempData["ErrorMessage"] = "Erro de banco de dados: " + sqlEx.Message;
            Console.WriteLine("SQL Exception: " + sqlEx);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.ToString();
            return View();
        }
        return View(formFunc);
    }

    public async Task<IActionResult> HomePage()
    {
        try
        {
            HomePageModel HPM = new HomePageModel();
            var prod = await _produtoRepository.GetAll();
            HPM.TotalProdutos = prod.Count();

            var cli = await _clienteRepository.GetAll();
            HPM.TotalClientes = cli.Count();

            var producoes = await _producaoRepository.GetAll();
            HPM.TotalProducoes = producoes.Count();
            HPM.ProducoesPendentes = producoes.Where(p => p.Finalizada == false).Count();
            HPM.ProducoesConcluidas = producoes.Where(p => p.Finalizada == true).Count();

            var pedidos = await _pedidosRepository.GetAll();
            HPM.TotalPedidos = pedidos.Count();

            return View(HPM);
        }catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return View();
        }
        
    }

    public IActionResult Sobre()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> Pedidos()
    {
        var pedidos = await _pedidosRepository.GetAll();
        var Clientes = await _clienteRepository.GetAll();
        PedidoView PDV = new PedidoView(pedidos,Clientes);

        return View(PDV);
    }

}
