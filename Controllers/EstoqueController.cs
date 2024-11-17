using Microsoft.AspNetCore.Mvc;
using agropindas.Repositories;
using agropindas.Models;

namespace agropindas.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ICrudRepository<Fornecedor> _fornecedor;
        private readonly ICrudRepository<Produto> _produtos;
        private readonly ICrudRepository<Compra> _compra;
        private readonly EstoqueRepository _lote;

        public EstoqueController(ICrudRepository<Fornecedor> f, ICrudRepository<Produto> p, ICrudRepository<Compra> c, EstoqueRepository lote)
        {
            _fornecedor = f;
            _produtos = p;
            _compra = c;
            _lote = lote;
        }
        public async Task<ActionResult> Index(string searchString)
        {
            //Rota para ver os lotes
            List<Lote> lotesView = null;
            if (string.IsNullOrWhiteSpace(searchString))
            {
                var Lotes = await _lote.GetAll();
                foreach (var l in Lotes)
                {
                    l.Produto = await _produtos.Get(l.IdProduto);
                }
                return View(Lotes);
            }
            else 
            {
                var searchprod = await _produtos.Get(searchString);
                var lotes = await _lote.GetAll(searchprod.Id.ToString());
                foreach(var l in lotes)
                {
                    l.Produto = searchprod;
                }
                return View(lotes);
            }
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
        public async Task<IActionResult> Compra(CompraViewModel CVM, List<Lote> Lotes)
        {
            try
            {
                //cadastrar compra
                DateOnly dateToday;
                CVM.Compra.DataCompra = DateTime.Now;

                if(CVM.Compra.IdFornecedor == 0)
                {
                    TempData["ErrorMessage"] = "Selecione um fornecedor";
                    return RedirectToAction("Compra");
                }

                await _compra.Add(CVM.Compra);

                Compra LastInsert = await _compra.Get("Palmeiras não tem Mundial");

                //cadastrar lotes
                foreach (Lote lote in Lotes)
                {
                    if (lote.IdProduto == 0)
                    {
                        TempData["ErrorMessage"] = "Selecione os Produtos Corretamente";
                        return RedirectToAction("Compra");
                    }
                    else
                    {
                        lote.IdCompra = LastInsert.Id;
                        await _lote.Add(lote);
                    }
                }

                //finalização
                TempData["SuccessMessage"] = "Compra cadastrada!";
                return RedirectToAction("HomePage", "Home");
            }
            catch (Exception ex) 
            {
                TempData["ErrorMessage"] = "Erro no processo, verifique o console";
                Console.WriteLine(ex.ToString());
                return RedirectToAction("HomePage", "Home");
            }
        }

        public async Task<IActionResult> Delete(int IdProduto, int IdCompra)
        {
            await _lote.Delete(IdProduto, IdCompra);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LotesProntos()
        {
            var view = await _lote.GetAllMudas();

            foreach(var p in view)
            {
                p.ProdutoMuda = await _produtos.Get(p.Id);
            }
            return View(view);
        } 
    }
}
