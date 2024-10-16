using agropindas.Models;
using agropindas.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


namespace agropindas.Controllers;
public class ProdutoController : Controller
{
    private readonly ICrudRepository<Produto> _ProdRepository;
    private readonly ISelectItems<ProdAssets> _unidadeRepository;

    public ProdutoController(ICrudRepository<Produto> p, ISelectItems<ProdAssets> u)
    {
        _ProdRepository = p;
        _unidadeRepository = u;
    }
    public async Task<IActionResult> Index(string searchString)
    {
        var unidades = await _unidadeRepository.GetAll("UnidadeCadastro");
        var tipos = await _unidadeRepository.GetAll("TipoProduto");
        try
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var produtos = await _ProdRepository.GetAll(searchString);
                foreach (var produto in produtos)
                {
                    foreach (var unidade in unidades)
                    {
                        if (produto.UnidadeCadastro == unidade.Id)
                        {
                            produto.OUnidadeCadastro = unidade;
                        }
                    }
                    foreach (var tipo in tipos)
                    {
                        if (produto.TipoProduto == tipo.Id)
                        {
                            produto.OTipoProduto = tipo;
                        }
                    }
                }
                return View(produtos);
            }
            else
            {
                var produtos = await _ProdRepository.GetAll();
                foreach (var produto in produtos)
                {
                    foreach (var unidade in unidades)
                    {
                        if (produto.UnidadeCadastro == unidade.Id)
                        {
                            produto.OUnidadeCadastro = unidade;
                        }
                    }
                    foreach (var tipo in tipos)
                    {
                        if (produto.TipoProduto == tipo.Id)
                        {
                            produto.OTipoProduto = tipo;
                        }
                    }
                }
                return View(produtos);
            }
        }
        catch (SqlException sqlex)
        {
            var produtosbkp = await _ProdRepository.GetAll();
            TempData["ErrorMessage"] = "Sistema identificou um problema de banco de dados ao realizar a pesquisa";
            Console.WriteLine(sqlex);
            return View(produtosbkp);
        }
        catch(Exception ex)
        {
            var produtosbkp = await _ProdRepository.GetAll();
            TempData["ErrorMessage"] = "Sistema identificou um erro ao realizar a pesquisa";
            Console.WriteLine(ex);
            return View(produtosbkp);
        }
        

    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        try
        {
            var unidades = await _unidadeRepository.GetAll("UnidadeCadastro");
            var tipos = await _unidadeRepository.GetAll("TipoProduto");
            ViewBag.Unidade = unidades;
            ViewBag.Tipos = tipos;
            return View();
        }
        catch (SqlException SqlEx)
        {
            TempData["ErrorMessage"] = SqlEx.ToString();
            return View();
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.ToString();
            return View();
        }

    }

    [HttpPost]
    public async Task<IActionResult> Create(Produto p)
    {
        try
        {
            await _ProdRepository.Add(p);
            TempData["SuccessMessage"] = "Produto cadastrado!";
            return RedirectToAction("Index");
        }
        catch (SqlException sqlex)
        {
            await Console.Out.WriteLineAsync(sqlex.ToString());
            TempData["ErrorMessage"] = "Erro ao Tentar adicionar produto na tabela, verifique o console";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.ToString();
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        {
            try
            {
                await _ProdRepository.Delete(id);
                TempData["SuccessMessage"] = "Produto cadastrado!";
                return RedirectToAction("Index");
            }
            catch (SqlException sqlex)
            {
                await Console.Out.WriteLineAsync(sqlex.ToString());
                TempData["ErrorMessage"] = "Erro ao tentar excluir produto na tabela, verifique o console";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.ToString();
                return RedirectToAction("Index");
            }
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
        var unidades = await _unidadeRepository.GetAll("UnidadeCadastro");
        var tipos = await _unidadeRepository.GetAll("TipoProduto");
        ViewBag.Unidade = unidades;
        ViewBag.Tipos = tipos;
        var ProdToEdit = await _ProdRepository.Get(Id);
        return View(ProdToEdit);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Produto p)
    {
        try
        {
            await _ProdRepository.Update(p);
            TempData["SuccessMessage"] = "Produto Editado!";
            return RedirectToAction("Index");
        }
        catch (SqlException sqlex)
        {
            await Console.Out.WriteLineAsync(sqlex.ToString());
            TempData["ErrorMessage"] = "Erro ao tentar editar produto na tabela, verifique o console";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.ToString();
            return RedirectToAction("Index");
        }
    }

}

 