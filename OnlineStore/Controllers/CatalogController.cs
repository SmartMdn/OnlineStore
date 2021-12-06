using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OnlineStore.WebUI.Data;
using OnlineStore.WebUI.Models;
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
        [Authorize(Policy = "OnlyForAuthenticateUser")]
        public async Task<ActionResult> Index(int? id)
        {

            id ??= 0;
            IQueryable<Product> products = _context.Products;

            if ( id != 0)
            {
                products = products.Where(p => p.CategoryId == id);
            }

            ProductsViewModel pvm = new ProductsViewModel
            {
                Products = await products.ToListAsync(),
                Categories = await _context.Categories.ToListAsync()
            };

            return View(pvm);
        }

    }
}
