using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nøsted_Serviceordre_Behandling.Data;
using Nøsted_Serviceordre_Behandling.Models;

namespace Nøsted_Serviceordre_Behandling.Controllers
{
    public class RapportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RapportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            try
            {
                var Serviceordre = await _context.Serviceordre
                    .Where(o => o.id == id)
                    .ToListAsync();

                return View(Serviceordre);
            }
            catch (Exception)
            {
                return Problem("Error", statusCode: 500);
            }
        }
    }

}




