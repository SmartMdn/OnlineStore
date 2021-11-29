using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore.WebUI.Data;
using OnlineStore.WebUI.Models.ViewModels;

namespace OnlineStore.WebUI.Controllers
{
    public class CatalogController : Controller
    {

        private readonly OnlineStoreContext _context;

        public CatalogController(OnlineStoreContext context)
        {
            _context = context;
        }


        // GET: CatalogController
        public async Task<ActionResult> Index()
        {
            ProductsViewModel pvm = new ProductsViewModel
            {
                Products = await _context.Products.ToListAsync(), 
                Categories = await _context.Categories.ToListAsync()
            };

            return View(pvm);
        }


    }
}
