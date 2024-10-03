using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using agropindas.Models;
using agropindas.Repositories;
using Microsoft.Data.SqlClient;

namespace agropindas.Controllers;
    public class FornecedorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
