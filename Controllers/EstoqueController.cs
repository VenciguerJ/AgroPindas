using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using agropindas.Repositories;
using agropindas.Models;
using Microsoft.Data.SqlClient;

namespace agropindas.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ICrudRepository<Fornecedor> _fornecedor;
        private readonly ICrudRepository<Produto> _produtos;
        private readonly ICrudRepository<Compra> _compra;
        private readonly ICrudRepository<Lote> _lote;

        public EstoqueController(ICrudRepository<Fornecedor> f, ICrudRepository<Produto> p, ICrudRepository<Compra> c, ICrudRepository<Lote> lote)
        {
            _fornecedor = f;
            _produtos = p;
            _compra = c;
            _lote = lote;
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

        [HttpPost]
        public async Task<IActionResult> FinalizarCompra(CompraViewModel CVM, List<Lote> Lotes)
        {
            try
            {
                //cadastrar compra
                DateOnly dateToday;
                CVM.Compra.DataCompra = DateTime.Now;
                await _compra.Add(CVM.Compra);



                //cadastrar lotes
                foreach (Lote lote in Lotes)
                {

                }

                //finalização
                TempData["SuccessMessage"] = "Compra cadastrada!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) 
            {
                TempData["ErrorMessage"] = "Erro no processo, verifique o console";
                return RedirectToAction(nameof(Index));
            }
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
