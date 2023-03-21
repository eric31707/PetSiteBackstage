using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PetSite.Models.EFModels;
using PetSite.Models.VM;
using System;

namespace PetSite.Controllers
{
	public class ProductsController : Controller
	{
		private readonly PetSiteContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ProductsController(PetSiteContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;

		}
		public async Task<IActionResult> Index()
		{
			var petSiteContext =await _context.Products.Include(p => p.Category).Include(p => p.Color).Include(p => p.Flavor).Include(p => p.Size).Include(p => p.Species).ToListAsync();
			var productvm = petSiteContext.Select(x=>x.ToEntity()).ToList();
			foreach (var n in productvm)
			{
				var productImages =_context.ProductImages.Where(x => x.ProductId == n.Id).ToList();
				n.ImageName = productImages.Select(x => x.Images).ToArray();
			}
			return View(productvm);
		}
        public async Task<IActionResult> Status0()
        {
            var petSiteContext = await _context.Products.Where(x => x.Status==false).Include(p => p.Category).Include(p => p.Color).Include(p => p.Flavor).Include(p => p.Size).Include(p => p.Species).ToListAsync();
            var productvm = petSiteContext.Select(x => x.ToEntity()).ToList();
			foreach (var n in productvm)
			{
				var productImages = _context.ProductImages.Where(x => x.ProductId == n.Id).ToList();
				n.ImageName = productImages.Select(x => x.Images).ToArray();
			}
			return View(productvm);
        }
		public async Task<IActionResult> Status1()
		{
			var petSiteContext = await _context.Products.Where(x => x.Status == true).Include(p => p.Category).Include(p => p.Color).Include(p => p.Flavor).Include(p => p.Size).Include(p => p.Species).ToListAsync();
			var productvm = petSiteContext.Select(x => x.ToEntity()).ToList();
			foreach (var n in productvm)
			{
				var productImages = _context.ProductImages.Where(x => x.ProductId == n.Id).ToList();
				n.ImageName = productImages.Select(x => x.Images).ToArray();
			}
			return View(productvm);
		}
		public IActionResult Create()
		{
			ViewData["CategoryId"] = new SelectList(_context.ChildCategories, "ChildCategoryId", "Name");
			ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "Name");
			ViewData["FlavorId"] = new SelectList(_context.Flavors, "FlavorId", "Name");
			ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "Name");
			ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "Name");
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductVM model)
		{
			var product = new Product()
			{
				ProductId = model.Id,
				Name = model.Name,
				Price = model.Price,
				Description = model.Description,
				CategoryId = model.CategoryId,
				Status = model.Status,
				SizeId = model.SizeId,
				ColorId = model.ColorId,
				FlavorId = model.FlavorId,
				SpeciesId = model.SpeciesId,
				Quantity = 0,
				CreateDate=DateTime.Now,
			};
			int num = 0;
			if (model.Image == null)
			{
				await _context.Products.AddAsync(product);
				await _context.SaveChangesAsync();
			}
			else{
				await _context.Products.AddAsync(product);
				await _context.SaveChangesAsync();
				var newProduct = _context.Products.FirstOrDefault(a => a.ProductId== product.ProductId);

				foreach (var items in model.Image)
				{
					string imageName = Guid.NewGuid().ToString() + ".png";
					string path = _webHostEnvironment.WebRootPath + "/ProductImages/" + imageName;
					using (var stream = System.IO.File.Create(path))
					{
						model.Image[num].CopyToAsync(stream);
						num++;
					}
					var newImage = new ProductImage()
					{
						ProductId = newProduct.ProductId,
						Images = imageName
					};
					await _context.ProductImages.AddAsync(newImage);
					await _context.SaveChangesAsync();

				}
			}
			
			return RedirectToAction("Index");
		}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
			var productvm = product.ToEntity();
			var productpics = _context.ProductImages.Where(x => x.ProductId == productvm.Id).ToList();
			productvm.ImageName = productpics.Select(x => x.Images).ToArray();

			ViewData["CategoryId"] = new SelectList(_context.ChildCategories, "ChildCategoryId", "Name", product.CategoryId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "Name", product.ColorId);
            ViewData["FlavorId"] = new SelectList(_context.Flavors, "FlavorId", "Name", product.FlavorId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "Name", product.SizeId);
            ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "Name", product.SpeciesId);
            return View(productvm);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,ProductVM product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
					var products = new Product()
					{
						ProductId = product.Id,
						Name = product.Name,
						Price = product.Price,
						Description = product.Description,
						CategoryId = product.CategoryId,
						Status = product.Status,
						SizeId = product.SizeId,
						ColorId = product.ColorId,
						FlavorId = product.FlavorId,
						SpeciesId = product.SpeciesId,
						Quantity = product.Quantity,
						CreateDate = product.CreateDate,
					};
					if (product.Image == null)
					{

						_context.Update(products);
						await _context.SaveChangesAsync();
					}
					else
					{
						_context.Update(products);
						await _context.SaveChangesAsync();
						var newProduct = _context.Products.OrderByDescending(p => p.ProductId).FirstOrDefault(a => a.ProductId == product.Id);
						int num = 0;
						foreach (var items in product.Image)
						{
							string imageName = Guid.NewGuid().ToString() + ".png";
							string path = _webHostEnvironment.WebRootPath + "/ProductImages/" + imageName;
							using (var stream = System.IO.File.Create(path))
							{
								product.Image[num].CopyToAsync(stream);
								num++;
							}
							var newImage = new ProductImage()
							{
								ProductId = newProduct.ProductId,
								Images = imageName
							};	
							await _context.ProductImages.AddAsync(newImage);
							_context.Update(products);
							await _context.SaveChangesAsync();
						}
					}
                
                return RedirectToAction(nameof(Index));
            
            //ViewData["CategoryId"] = new SelectList(_context.ChildCategories, "ChildCategoryId", "Name", product.CategoryId);
            //ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "Name", product.ColorId);
            //ViewData["FlavorId"] = new SelectList(_context.Flavors, "FlavorId", "Name", product.FlavorId);
            //ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "Name", product.SizeId);
            //ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "Name", product.SpeciesId);
            //return View(product);   
        }
        
        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'PetSiteContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
			
			if (product != null)
            {
                _context.Products.Remove(product);
			}
			
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateStatusTo1(int id)
		{
			if (_context.Products == null)
			{
				return Problem("Entity set 'PetSiteContext.Products'  is null.");
			}
			var product = await _context.Products.FindAsync(id);
			product.Status = true;

			if (product != null)
			{
				_context.Products.Update(product);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Status0));
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateStatusTo0(int id)
		{
			if (_context.Products == null)
			{
				return Problem("Entity set 'PetSiteContext.Products'  is null.");
			}
			var product = await _context.Products.FindAsync(id);
			product.Status = false;

			if (product != null)
			{
				_context.Products.Update(product);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Status1));
		}
		public async Task<string> ImgDelete(string img)
		{

			string sourceDir = _webHostEnvironment.WebRootPath + "/ProductImages/" + img;
			System.IO.File.Delete(sourceDir);
			var deleteimg = await _context.ProductImages.Where(x => x.Images == img).FirstOrDefaultAsync();
			_context.ProductImages.Remove(deleteimg);
			await _context.SaveChangesAsync();
			return ("已刪除");

		}
		private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
