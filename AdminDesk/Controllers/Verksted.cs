using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdminDesk.Models;

namespace AdminDesk.Controllers;

public class VerkstedController : Controller
{
    private readonly ILogger<VerkstedController> _logger;

    public VerkstedController(ILogger<VerkstedController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    
}

