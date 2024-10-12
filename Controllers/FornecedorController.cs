using Microsoft.AspNetCore.Mvc;
using agropindas.Models;
using agropindas.Repositories;
using Microsoft.Data.SqlClient;

namespace agropindas.Controllers;
    public class FornecedorController : Controller
    {
        private readonly ICrudRepository<Fornecedor> _crudRepository;
        public FornecedorController(ICrudRepository<Fornecedor> forn)
        {
            _crudRepository = forn;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var fornecedores = await _crudRepository.GetAll();
            return View(fornecedores);
        }

    [HttpGet]
    public async Task<IActionResult> Edit(Fornecedor f)
    {
        var fonecedorDB = await _crudRepository.Get(f.Id);
        return View(fonecedorDB); 
    }
    [HttpPost]
    public async Task<IActionResult> EditSql(Fornecedor f)
    {
        try
        {
            var funcIdCorreto = await _crudRepository.Get(f.CNPJ); // pegando o id correto pois na mudança de rotas ele perde o ID, no formulário não podemos manipular o id, por isso id aqui é 0
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
