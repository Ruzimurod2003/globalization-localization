using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyGlobalization.Data;
using EasyGlobalization.Models;
using EasyGlobalization.ViewModels;

namespace EasyGlobalization.Controllers
{
    public class CulturesController : Controller
    {
        private readonly LocalizationContext _context;

        public CulturesController(LocalizationContext context)
        {
            _context = context;
        }

        // GET: Cultures
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cultures.ToListAsync());
        }


        // POST: Cultures/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CultureCreateVM viewModel)
        {
            if (ModelState.IsValid)
            {
                Culture culture = new Culture { Name = viewModel.Name };
                _context.Add(culture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }


        // POST: Cultures/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CultureEditVM viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Culture culture = new Culture { Id = viewModel.Id, Name = viewModel.Name };
                    _context.Update(culture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CultureExists(viewModel.Id))
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
            return View(viewModel);
        }

        // POST: Cultures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cultures == null)
            {
                return Problem("Entity set 'LocalizationContext.Cultures'  is null.");
            }
            var culture = await _context.Cultures.FindAsync(id);
            if (culture != null)
            {
                _context.Cultures.Remove(culture);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CultureExists(int id)
        {
            return _context.Cultures.Any(e => e.Id == id);
        }
    }
}
