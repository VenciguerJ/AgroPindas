using agropindas.Models;
using agropindas.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace agropindas.Controllers;
public class ProdutoController : Controller
{
    private readonly ICrudRepository<Produto> _ProdRepository;
    private readonly ICrudRepository<UnidadeCadastro> _unidadeRepository;


    public ProdutoController(ICrudRepository<Produto> p, ICrudRepository<UnidadeCadastro> u)
    {
        _ProdRepository = p;
        _unidadeRepository = u;
    }
    public async Task<IActionResult> Index()
    {
        var produtos = await _ProdRepository.GetAll();
        return View(produtos);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var unidades = await _unidadeRepository.GetAll();
        ViewBag.Unidade = unidades;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Produto p)
    {
        try
        {
            await _ProdRepository.Add(p);
            TempData["SuccesMessage"] = "Produto cadastrado!";
            return View();
        }
        catch(Exception ex)
        {
            TempData["ErrorMessage"] = ex.ToString();
            return View();
        }
        
    }

}

