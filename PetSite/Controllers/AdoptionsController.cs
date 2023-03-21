using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetSite.Models.EFModels;

namespace PetSite.Controllers
{
    public class AdoptionsController : Controller
    {
        private readonly PetSiteContext _context;

        public AdoptionsController(PetSiteContext context)
        {
            _context = context;
        }

        // GET: Adoptions
        public async Task<IActionResult> Index()
        {
            var petSiteContext = _context.Adoptions.Include(a => a.AnimalGender)
                .Include(a => a.AnimalSize)
                .Include(a => a.AnimalType)
                .Include(a => a.Member);
            return View(await petSiteContext.ToListAsync());
        }

        // GET: Adoptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Adoptions == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoptions
                .Include(a => a.AnimalGender)
                .Include(a => a.AnimalSize)
                .Include(a => a.AnimalType)
                .Include(a => a.Member)
                .FirstOrDefaultAsync(m => m.AdoptionId == id);
            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }

        // GET: Adoptions/Create
        public IActionResult Create()
        {
            ViewData["AnimalGenderId"] = new SelectList(_context.Genders, "GenderId", "GenderType");
            ViewData["AnimalSizeId"] = new SelectList(_context.AnimalSizes, "AnimalSizeId", "AnimalSize1");
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimailTypes, "AnimailTypeId", "AnimailType1");
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Account");
            return View();
        }

        // POST: Adoptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdoptionId,MemberId,AdoptName,AnimalGenderId,AnimalTypeId,AnimalColor,AreaAddress,AdoptTitle,AdoptDescription,AnimalSizeId,Image1,Image2,Image3,Image4,Image5,ContainNumber,OpenAdopt,Ligation,Deworming,Vaccination,Triple,AnimalChip,Fiv,FeLv")] Adoption adoption)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adoption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalGenderId"] = new SelectList(_context.Genders, "GenderId", "GenderType", adoption.AnimalGenderId);
            ViewData["AnimalSizeId"] = new SelectList(_context.AnimalSizes, "AnimalSizeId", "AnimalSize1", adoption.AnimalSizeId);
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimailTypes, "AnimailTypeId", "AnimailType1", adoption.AnimalTypeId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Account", adoption.MemberId);
            return View(adoption);
        }

        // GET: Adoptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Adoptions == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoptions.FindAsync(id);
            if (adoption == null)
            {
                return NotFound();
            }
            ViewData["AnimalGenderId"] = new SelectList(_context.Genders, "GenderId", "GenderType", adoption.AnimalGenderId);
            ViewData["AnimalSizeId"] = new SelectList(_context.AnimalSizes, "AnimalSizeId", "AnimalSize1", adoption.AnimalSizeId);
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimailTypes, "AnimailTypeId", "AnimailType1", adoption.AnimalTypeId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Account", adoption.MemberId);
            return View(adoption);
        }

        // POST: Adoptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdoptionId,MemberId,AdoptName,AnimalGenderId,AnimalTypeId,AnimalColor,AreaAddress,AdoptTitle,AdoptDescription,AnimalSizeId,Image1,Image2,Image3,Image4,Image5,ContainNumber,OpenAdopt,Ligation,Deworming,Vaccination,Triple,AnimalChip,Fiv,FeLv")] Adoption adoption)
        {
            if (id != adoption.AdoptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adoption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdoptionExists(adoption.AdoptionId))
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
            ViewData["AnimalGenderId"] = new SelectList(_context.Genders, "GenderId", "GenderType", adoption.AnimalGenderId);
            ViewData["AnimalSizeId"] = new SelectList(_context.AnimalSizes, "AnimalSizeId", "AnimalSize1", adoption.AnimalSizeId);
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimailTypes, "AnimailTypeId", "AnimailType1", adoption.AnimalTypeId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Account", adoption.MemberId);
            return View(adoption);
        }

        // GET: Adoptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Adoptions == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoptions
                .Include(a => a.AnimalGender)
                .Include(a => a.AnimalSize)
                .Include(a => a.AnimalType)
                .Include(a => a.Member)
                .FirstOrDefaultAsync(m => m.AdoptionId == id);
            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }

        // POST: Adoptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Adoptions == null)
            {
                return Problem("Entity set 'PetSiteContext.Adoptions'  is null.");
            }
            var adoption = await _context.Adoptions.FindAsync(id);
            if (adoption != null)
            {
                _context.Adoptions.Remove(adoption);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionExists(int id)
        {
          return _context.Adoptions.Any(e => e.AdoptionId == id);
        }
    }
}
