using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminDesk.Data;
using AdminDesk.Models;

namespace AdminDesk.Controllers
{
    public class ServiceordresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceordresController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Serviceordre != null
                ? View(await _context.Serviceordre.ToListAsync())
                : Problem("Entity set 'ApplicationDbContext.Serviceordre' is null.");
        }

        public IActionResult Create()
        {
            return View();
        }

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
    }
}
