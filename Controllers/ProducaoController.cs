using agropindas.Models;
using agropindas.Repositories;
using Microsoft.AspNetCore.Mvc;

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
            var producaoView = await _producao.Get(sup.Id.ToString());
            var fertilizantes = await _estoque.GetAllFertilizantes(sup.Id);

            ProducaoViewModel view = new ProducaoViewModel(sup, produtosView, producaoView);
           
            view.fertilizantesView = fertilizantes;

            if(view.ProducaoCalha != null)
            {
                view.LoteView = new LoteMuda();
                view.LoteView.IdProducao = view.ProducaoCalha.Id;
                view.LoteView.IdEstoqueProduto = view.ProducaoCalha.IdProdutoProduzido;
                foreach (var p in produtosView) 
                {
                    if(view.LoteView.IdEstoqueProduto == p.Id)
                    {
                        view.LoteView.ProdutoMuda = p;
                    }
                }
                view.LoteView.QuantidadeInicial = view.ProducaoCalha.QuantidadeProduzido;
            }
            view.fertilizantesView = await _estoque.GetAllFertilizantes(view.SuporteProducao.Id);
            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduction(ProducaoViewModel PVM, List<Fertilizante>? Fertilizantes)
        {
            try
            {
                //suporteCalha
                if (PVM.ProducaoCalha != null) // valida se teve producao ao alterar
                {
                    PVM.SuporteProducao.ocupada = true;
                }

                await _estoque.UpdateSuporte(PVM.SuporteProducao);

                //producaoCalha

                var lotes = await _estoque.GetAll(PVM.ProducaoCalha.IdProdutoProduzido.ToString());
                var lote = new Lote();
                foreach(var l in lotes)
                {
                    if (PVM.ProducaoCalha.IdLoteUsado == l.IdCompra) // vê qual é o produto da calha nmovamente
                    {
                        lote = l;
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

                lote.QuantidadeSaida +=  PVM.ProducaoCalha.QuantidadeProduzido;
                lote.QuantidadeLote -= lote.QuantidadeSaida;

                await _estoque.Update(lote);

                //define dia da colheita 

                var prod = await _produtos.Get(PVM.ProducaoCalha.IdProdutoProduzido);
                PVM.ProducaoCalha.DiaColheita = DateTime.Now.AddDays(prod.DiasColheita);
                PVM.ProducaoCalha.Finalizada = false;

                await _producao.Add(PVM.ProducaoCalha);

                //Fertilizantes 

                await _estoque.DeleteFertilizante(PVM.SuporteProducao.Id);
                foreach(var f in Fertilizantes)
                {
                    f.IdSuporte = PVM.SuporteProducao.Id;
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

                var fertilizantes = await _estoque.GetAllFertilizantes(id);

                foreach(var f in fertilizantes)
                {
                    await _estoque.DeleteFertilizante(f.IdSuporte);
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

        public async Task<IActionResult> Colheita(LoteMuda l)
            {
            try
            {
                var producaoPronta = await _producao.Get(l.IdProducao);
                producaoPronta.Finalizada = true;
                await  _producao.Update(producaoPronta);
                await _estoque.Colheita(l);

                TempData["SuccessMessage"] = "Colheita inclusa com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro na iclusão, verifique o console";
                Console.WriteLine(ex.ToString());

                return RedirectToAction("Index");

            }
        }
    }
}   