using Microsoft.AspNetCore.Mvc;
using agropindas.Repositories;
using agropindas.Models;

namespace agropindas.Controllers
{

    public class APIController : Controller
    {
        private readonly EstoqueRepository _estoque;
        private readonly ICrudRepository<Produto> _produtos;
        
        public APIController(EstoqueRepository e, ICrudRepository<Produto> p)
        {
            _estoque = e;
            _produtos = p;
        }

        //AJAX FUNCTION
        [HttpGet]
        public async Task<JsonResult> GetLotesByProduto(int produtoId)
        {
            // Busca os lotes associados ao produto selecionado
            var lotes = await _estoque.GetAll(produtoId.ToString());

            // Retorna a lista de lotes em formato JSON
            return Json(lotes);
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
