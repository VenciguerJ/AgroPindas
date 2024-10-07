using System.Diagnostics;
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
    public async Task<IActionResult> Edit()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Delete()
    {
        return View();
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
