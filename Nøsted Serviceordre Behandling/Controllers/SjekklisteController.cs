using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nøsted_Serviceordre_Behandling.Data;
using Nøsted_Serviceordre_Behandling.Models;

namespace Nøsted_Serviceordre_Behandling.Controllers
{
    public class SjekklisteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SjekklisteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sjekkliste
        public async Task<IActionResult> Index()
        {
            return _context.Serviceordre != null ?
                        View(await _context.Serviceordre.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Serviceordre'  is null.");
        }

    }

}

