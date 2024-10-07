using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using agropindas.Models;
using agropindas.Repositories;
using Microsoft.Data.SqlClient;

namespace agropindas.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ILogin<Funcionario> _login;
    private readonly ICrudRepository<Funcionario> _crudRepository;

    public HomeController(ILogger<HomeController> logger, ICrudRepository<Funcionario> FuncionarioRepository, ILogin<Funcionario> Ilogin)
    {
        _logger = logger;
        _login = Ilogin;
        _crudRepository = FuncionarioRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        Console.WriteLine("Passou por aqui");
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

    public IActionResult HomePage()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
