using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OnlineStore.WebUI.Data;
using OnlineStore.WebUI.Models;
using OnlineStore.WebUI.Models.ViewModels;
using static OnlineStore.WebUI.Controllers.AccountController;

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


        public async Task<IActionResult> AddInBasket(int ProductId)
        {
            IQueryable<Product> products = _context.Products;
            var product = await _context.Products.FindAsync(ProductId);
            if (User.Identity != null)
            {
                if (User.Identity.Name != null)
                {
                    var user = await _context.Users.FindAsync(int.Parse(User.Identity.Name));
                    product.CartId = user.CartId;
                }
            }

            

            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
