using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineStore.WebUI.Data;
using OnlineStore.WebUI.Migrations;
using OnlineStore.WebUI.Models;
using OnlineStore.WebUI.Models.ViewModels;
using SharpCompress.Compressors.PPMd;

namespace OnlineStore.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly OnlineStoreContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public ProductsController(OnlineStoreContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Products
        [Authorize(Policy = "OnlyForAdmin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: Products/Details/5
        [Authorize(Policy = "OnlyForAdmin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Policy = "OnlyForAdmin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ProductionDateTime,Price,Description,CategoryId")] Product product, IFormFile uploadedFile)
        {
            if (!ModelState.IsValid) return View(product);
            if (!_context.Categories.Any(o => o.Id == product.CategoryId)) return View(product);
            if (uploadedFile == null) return View(product);
            // путь к папке Files
            var path = "/files/pictures/products/" + uploadedFile.FileName;
            if (_context.Files.Any(f => f.Path == path)) return View(product);  //Проверка повторяющихся путей
            // сохраняем файл в папку Files в каталоге wwwroot
            
            await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            
            FileModel file = new FileModel { Name = product.Title+".jpg", Path = path };
            _context.Files.Add(file);
            product.File = path;
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Products/Edit/5
        [Authorize(Policy = "OnlyForAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ProductionDateTime,Price,Description,CategoryId")] Product product, IFormFile uploadedFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Policy = "OnlyForAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("OnlyForAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);


        }
        
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile == null) return RedirectToAction("Index");
            // путь к папке Files
            var path = "/Files/" + uploadedFile.FileName;
            // сохраняем файл в папку Files в каталоге wwwroot
            await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
            _context.Files.Add(file);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
