using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore.WebUI.Data;
using OnlineStore.WebUI.Models;
using OnlineStore.WebUI.Models.ViewModels;

namespace OnlineStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly OnlineStoreContext _context;

        public CartController(OnlineStoreContext context)
        {
            _context = context;

        }


        public async Task<IActionResult> Index()
        {
            if (User.Identity == null) return RedirectToAction(nameof(EmptyCart));
            if (User.Identity.Name == null) return RedirectToAction(nameof(EmptyCart));

            var user = await _context.Users.FindAsync(int.Parse(User.Identity.Name));
            var products =
                _context.Products.Where(p => p.CartId == user.CartId);

            ProductsViewModel pvm = new()
            {
                Products = products,
                Categories = await _context.Categories.ToListAsync()
            };


            return View(pvm);

        }

        public IActionResult EmptyCart()
        {
            return View();

        }

        public async Task<IActionResult>  SubbmitOrder()
        {
            IQueryable<Product> products = _context.Products;
            if (User.Identity == null) return RedirectToAction(nameof(EmptyCart));
            if (User.Identity.Name == null) return RedirectToAction(nameof(EmptyCart));
            
            var user = await _context.Users.FindAsync(int.Parse(User.Identity.Name));
            while (_context.Products.Any(p=> user.CartId == p.CartId))
            { 
                var product = _context.Products.FirstOrDefault(o => o.CartId == user.CartId);
                if (product != null) _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(EmptyCart));
        }
    }
}
