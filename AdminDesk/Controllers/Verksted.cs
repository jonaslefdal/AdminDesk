using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminDesk.Entities;
using AdminDesk.Models;
using AdminDesk.Repositories;
using AdminDesk.Models.ServiceOrder;

namespace AdminDesk.Controllers
{
    public class Verksted : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}