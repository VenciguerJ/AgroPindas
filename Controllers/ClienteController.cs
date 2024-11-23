using agropindas.Models;
using agropindas.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace agropindas.Controllers;

public class ClienteController : Controller
{
    private readonly ICrudRepository<Cliente> _clienteRepository;

    public ClienteController(ICrudRepository<Cliente> clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    // Get all clientes
    public async Task<IActionResult> Index()
    {
        var clientes = await _clienteRepository.GetAll();
        return View(clientes);
    }

    // Get cliente by CPF
    public async Task<IActionResult> DetailsByCpf(string cpf)
    {
        var cliente = await _clienteRepository.Get(cpf);
        if (cliente == null)
        {
            return NotFound("Cliente não encontrado.");
        }
        return View(cliente);
    }

    // Get cliente by ID
    public async Task<IActionResult> Details(int id)
    {
        var cliente = await _clienteRepository.Get(id);
        if (cliente == null)
        {
            return NotFound("Cliente não encontrado.");
        }
        return View(cliente);
    }

    // Create cliente
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            await _clienteRepository.Add(cliente);
            return RedirectToAction(nameof(Index));
        }
        return View(cliente);
    }

    // Edit cliente
    public async Task<IActionResult> Edit(int id)
    {
        var cliente = await _clienteRepository.Get(id);
        if (cliente == null)
        {
            return NotFound("Cliente não encontrado.");
        }
        return View(cliente);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Cliente cliente)
    {
        if (id != cliente.Id)
        {
            return BadRequest("ID inválido.");
        }

        if (ModelState.IsValid)
        {
            await _clienteRepository.Update(cliente);
            return RedirectToAction(nameof(Index));
        }

        return View(cliente);
    }

    // Delete cliente
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await _clienteRepository.Delete(id);
        TempData["SuccessMessage"] = "Cliente Excluso";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var cliente = await _clienteRepository.Get(id);
        if (cliente != null)
        {
            await _clienteRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        return NotFound("Cliente não encontrado.");
    }
}
