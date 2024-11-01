using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using agropindas.Repositories;
using agropindas.Models;

namespace agropindas.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ICrudRepository<Fornecedor> _fornecedor;
        private readonly ICrudRepository<Produto> _produtos;

        public EstoqueController(ICrudRepository<Fornecedor> f, ICrudRepository<Produto> p)
        {
            _fornecedor = f;
            _produtos = p;
        }
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Compra()
        {
            var fornecedoresView = await _fornecedor.GetAll();
            var produtosView = await _produtos.GetAll();
            CompraViewModel model = new CompraViewModel(fornecedoresView, produtosView);
            return View(model);
        }


        //AJAX FUNCTION
        [HttpGet]
        public async Task<IActionResult> GetProduto(int id)
        {
            var prod = await _produtos.Get(id);

            if (prod == null)
            {
                return NotFound();
            }

            return Json(new { preco = prod.ValorProduto }); // Encapsula o valor em um objeto
        }

    }
}
