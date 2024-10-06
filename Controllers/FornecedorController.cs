using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace agropindas.Controllers
{
    public class FornecedorController : Controller
    {
        // GET: FornecedorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FornecedorController/Details/
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FornecedorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FornecedorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FornecedorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FornecedorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FornecedorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FornecedorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
