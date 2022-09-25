using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EaasyGlobalization.Data;
using EaasyGlobalization.Models;
using EaasyGlobalization.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.CodeAnalysis.FlowAnalysis;

namespace EaasyGlobalization.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly LocalizationContext _context;

        public ResourcesController(LocalizationContext context)
        {
            _context = context;
        }

        // GET: Resources
        public async Task<IActionResult> Index()
        {
            return View(await _context.Resources.ToListAsync());
        }

        // POST: Resources/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResourceCreateVM viewModel)
        {
            if (ModelState.IsValid)
            {
                int cultureId = _context.Cultures.FirstOrDefault(i => i.Name == viewModel.CultureName).Id;
                Resource resource = new Resource()
                {
                    Key = viewModel.Key,
                    Value = viewModel.Value,
                    CulutureId = cultureId
                };
                _context.Add(resource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }


        // POST: Resources/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResourceEditVM viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    int cultureId = _context.Cultures.First(i => i.Name == viewModel.CultureName).Id;
                    Resource resource = new Resource()
                    {
                        Id=viewModel.Id,
                        Key = viewModel.Key,
                        Value = viewModel.Value,
                        CulutureId = cultureId
                    };
                    _context.Update(resource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceExists(viewModel.Id))
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

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Resources == null)
            {
                return Problem("Entity set 'LocalizationContext.Resources'  is null.");
            }
            var resource = await _context.Resources.FindAsync(id);
            if (resource != null)
            {
                _context.Resources.Remove(resource);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourceExists(int id)
        {
            return _context.Resources.Any(e => e.Id == id);
        }
        [HttpPost]
        public IActionResult SetLanguage(string cultureName, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureName)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
