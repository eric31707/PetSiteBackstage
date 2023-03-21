using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSite.Models;
using PetSite.Models.DTOs;
using PetSite.Models.EFModels;
using PetSite.Models.Repositories;
using PetSite.Models.Services;
using PetSite.Models.ViewModels;

namespace PetSite.Controllers
{
    public class SupplierController : Controller
    {
        private readonly PetSiteContext _context;

        public SupplierController(PetSiteContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = new SupplierRepository(_context).GetAll();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SupplierCreateVM model)
        {
            try
            {
                new SupplierService(_context).Create(model.ToDTO());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var supplier = _context.Suppliers.SingleOrDefault(x => x.SupplierId == id).ToDTO();
            var model = supplier.ToCreateVM();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(SupplierCreateVM model)
        {
            try
            {
                new SupplierService(_context).UpdateProfile(model.ToDTO());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var supplier = _context.Suppliers.SingleOrDefault(x => x.SupplierId == id).ToDTO();
            var model = supplier.ToCreateVM();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(SupplierCreateVM model)
        {
            try
            {
                new SupplierService(_context).Delete(model.ToDTO());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            return RedirectToAction("Index");
        }
    }
}
