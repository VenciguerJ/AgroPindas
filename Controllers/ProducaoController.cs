using agropindas.Models;
using agropindas.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.CodeAnalysis;
namespace agropindas.Controllers
{
    public class ProducaoController : Controller
    {
        private readonly EstoqueRepository _estoque;
        private readonly ICrudRepository<Produto> _produtos;
        private readonly ICrudRepository<Producao> _producao;
        public ProducaoController(EstoqueRepository e, ICrudRepository<Produto> p, ICrudRepository<Producao> prd)
        {
            _estoque = e;
            _produtos = p;
            _producao = prd;

        }
        public async Task<IActionResult> Index()
        {
            var suportes = await _estoque.GetAllCalhas();
            return View(suportes);
        }

        public IActionResult CreateArea()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArea(SuporteCalha s)
        {
            await _estoque.AddSuporte(s);
            TempData["SuccessMessage"] = "Suporte Criado com Sucesso!";
            return RedirectToAction("Index");
        }
    
        public async Task<IActionResult> EditArea(int id)
        {
            var sup = await _estoque.GetCalha(id);
            var produtosView = await _produtos.GetAll();
            var producaoView = await _producao.Get(sup.Id);
            ProducaoViewModel view = new ProducaoViewModel(sup, produtosView, producaoView);
            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduction(ProducaoViewModel PVM, List<Fertilizante>? Fertilizantes)
        {
            try
            {
                //suporteCalha
                await _estoque.UpdateSuporte(PVM.SuporteProducao);

                //producaoCalha

                var lotes = await _estoque.GetAll(PVM.ProducaoCalha.IdProdutoProduzido.ToString());
                var lote = new Lote();
                foreach(var l in lotes)
                {
                    if (PVM.ProducaoCalha.IdLoteUsado == l.IdCompra) // vê qual é o produto da calha nmovamente
                    {
                        lote = l; break;
                    }
                }

                if(PVM.ProducaoCalha.QuantidadeProduzido > (lote.QuantidadeLote - lote.QuantidadeSaida))
                {
                    TempData["ErrorMessage"] = "A quantidade disponível do lote não é suficiente para fazer esta produção";
                    return View(PVM);
                }
                if(PVM.ProducaoCalha.QuantidadeProduzido > PVM.SuporteProducao.CapacidadeMudas)
                {
                    TempData["ErrorMessage"] = "A quantidade Da area total não é suficiente para fazer esta produção";
                    return View(PVM);
                }

                lote.QuantidadeSaida -= PVM.ProducaoCalha.QuantidadeProduzido;

                await _estoque.Update(lote);

                //define dia da colheita.
                var prod = await _produtos.Get(PVM.ProducaoCalha.IdProdutoProduzido);
                PVM.ProducaoCalha.DiaColheita = DateTime.Now.AddDays(prod.DiasColheita);

                await _producao.Add(PVM.ProducaoCalha);



                //Fertilizantes 
                foreach(var f in Fertilizantes)
                {
                   await _estoque.AddFertilizanteCalha(f);
                }
                TempData["SuccessMessage"] = "Alterações salvas";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao Realizar operação, consulte o console do desenvolvedor";
                Console.WriteLine(ex.ToString());
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> DeleteSuporte(int id)
        {
            try
            {
                var productions = await _producao.GetAll(id.ToString());
                foreach(var prod in productions) 
                {
                    await _producao.Delete(prod.Id);
                }
                await _estoque.DeleteSuporte(id);
                TempData["SuccessMessage"] = "Suporte excluso!";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                TempData["ErrorMessage"] = "Erro ao Realizar operação, consulte o console do desenvolvedor";
                return RedirectToAction("Index");
            }
        }
    }
}   
