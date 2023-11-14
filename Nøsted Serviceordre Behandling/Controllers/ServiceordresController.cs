using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nøsted_Serviceordre_Behandling.Data;
using Nøsted_Serviceordre_Behandling.Models;

namespace Nøsted_Serviceordre_Behandling.Controllers
{

    public class AllowAnonymousIfAuthenticatedAttribute : TypeFilterAttribute
    {
        public AllowAnonymousIfAuthenticatedAttribute() : base(typeof(AllowAnonymousIfAuthenticatedFilter))
        {
        }
    }

    public class AllowAnonymousIfAuthenticatedFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // If the user is authenticated, allow access
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            // If not authenticated, redirect to login
            var loginUrl = "/Identity/Account/Login";
            var returnUrl = context.HttpContext.Request.Path;
            context.Result = new RedirectResult($"{loginUrl}?ReturnUrl={returnUrl}");
        }
    }
    public class ServiceordresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceordresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Serviceordres
        public async Task<IActionResult> Index()
        {
            return _context.Serviceordre != null ?
                        View(await _context.Serviceordre.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Serviceordre'  is null.");
        }

        // GET: Serviceordres/ShowSearchForms

        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // GET: Serviceordres/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Serviceordre.Where(j => j.ServiceordreStatus.Contains
            (SearchPhrase)).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Serviceordre == null)
            {
                return NotFound();
            }

            var serviceordre = await _context.Serviceordre
                .FirstOrDefaultAsync(m => m.id == id);
            if (serviceordre == null)
            {
                return NotFound();
            }

            return View(serviceordre);
        }

        // GET: Serviceordres/Create
        [AllowAnonymousIfAuthenticated]
        public IActionResult Create()
        {
            return View();
        }


        // POST: Serviceordres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [AllowAnonymousIfAuthenticated]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Ordrenummer,Navn,Email,Telefon,Mottatt_dato,Adresse,ServiceordreStatus,Arbeidstimer,Reparatør")] Serviceordre serviceordre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceordre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceordre);
        }

        // GET: Serviceordres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Serviceordre == null)
            {
                return NotFound();
            }

            var serviceordre = await _context.Serviceordre.FindAsync(id);
            if (serviceordre == null)
            {
                return NotFound();
            }
            return View(serviceordre);
        }

        // POST: Serviceordres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Ordrenummer,Navn,Email,Telefon,Mottatt_dato,Adresse,ServiceordreStatus,Arbeidstimer,Reparatør")] Serviceordre serviceordre)
        {
            if (id != serviceordre.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceordre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceordreExists(serviceordre.id))
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
            return View(serviceordre);
        }

        // GET: Serviceordres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Serviceordre == null)
            {
                return NotFound();
            }

            var serviceordre = await _context.Serviceordre
                .FirstOrDefaultAsync(m => m.id == id);
            if (serviceordre == null)
            {
                return NotFound();
            }

            return View(serviceordre);
        }

        // POST: Serviceordres/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Serviceordre == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Serviceordre'  is null.");
            }
            var serviceordre = await _context.Serviceordre.FindAsync(id);
            if (serviceordre != null)
            {
                _context.Serviceordre.Remove(serviceordre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceordreExists(int id)
        {
            return (_context.Serviceordre?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
