using Microsoft.AspNetCore.Mvc;
using agropindas.Models;
using agropindas.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.CodeAnalysis.FlowAnalysis;

namespace agropindas.Controllers;
    public class FornecedorController : Controller
    {
        private readonly ICrudRepository<Fornecedor> _crudRepository;
        public FornecedorController(ICrudRepository<Fornecedor> forn)
        {
            _crudRepository = forn;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    var fornecedores = await _crudRepository.GetAll(searchString);
                    return View(fornecedores);
                }
                else
                {
                    var fornecedores = await _crudRepository.GetAll();
                    return View(fornecedores);
                }
            }
            catch (SqlException sqlex)
            {
                var fornecedoresbkp = await _crudRepository.GetAll();
                TempData["ErrorMessage"] = "Sistema identificou um problema de banco de dados ao realizar a pesquisa";
                Console.WriteLine(sqlex);
                return View(fornecedoresbkp);
            }
            catch (Exception ex)
            {
                var fornecedoresbkp = await _crudRepository.GetAll();
                TempData["ErrorMessage"] = "Sistema identificou um erro ao realizar a pesquisa";
                Console.WriteLine(ex);
                return View(fornecedoresbkp);
            }
            
        }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var fonecedorDB = await _crudRepository.Get(id);
        return View(fonecedorDB); 
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Fornecedor f)
    {
        try
        {
            var funcIdCorreto = await _crudRepository.Get(f.CNPJ); 
            f.Id = funcIdCorreto.Id;
            await _crudRepository.Update(f);
            TempData["SuccessMessage"] = "Fornecedor alterado com sucesso!";
            return RedirectToAction("Index");
        }
        catch (SqlException sqlex)
        {
            TempData["ErrorMessage"] = sqlex.ToString;
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.ToString;
            return RedirectToAction("Index");
        }
    }
    [HttpGet]
    public async Task<IActionResult> Delete(Fornecedor fornecedor)
    {
        await _crudRepository.Delete(fornecedor.Id);
        TempData["SuccessMessage"] = "Fornecedor Excluído com sucesso!!";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]

    public async Task<IActionResult> Create(Fornecedor formFornecedor)
    {
        if(!ModelState.IsValid)
        {
            return View();
        }

        try
        {
            await _crudRepository.Add(formFornecedor);
            return RedirectToAction("Index");

        }
        catch(SqlException Sqlex)
        {
            Console.WriteLine(Sqlex.Message);
            TempData["ErrorMessage"] = "Erro de SQL";
            return View();
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex;
            return View();
        }
    }
}
