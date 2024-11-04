using agropindas.Models;
using agropindas.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace agropindas.Controllers
{
    public class ProducaoController : Controller
    {
        private readonly EstoqueRepository _estoque;
        private readonly ICrudRepository<Produto> _produtos;
        public ProducaoController(EstoqueRepository e, ICrudRepository<Produto> p)
        {
            _estoque = e;
            _produtos = p;
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
            ProducaoViewModel view = new ProducaoViewModel(sup, produtosView);
            return View(view);
        }


    }
}   
