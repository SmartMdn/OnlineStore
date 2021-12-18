using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using OnlineStore.WebUI.Data;
using OnlineStore.WebUI.Models;
using OnlineStore.WebUI.Models.ViewModels;

namespace OnlineStore.WebUI.Controllers
{
    public class CatalogController : Controller
    {

        private readonly OnlineStoreContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public CatalogController(OnlineStoreContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
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
                Categories = await _context.Categories.ToListAsync(),
                Files = await _context.Files.ToListAsync()
            };

            return View(pvm);
        }


        public async Task<IActionResult> AddInBasket(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return RedirectToAction(nameof(Index));
            if (User.Identity is {Name: { }})
            {
                var user = await _context.Users.FindAsync(int.Parse(User.Identity.Name));
                if (user == null) return RedirectToAction(nameof(Index));
                product.CartId = user.CartId;
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
