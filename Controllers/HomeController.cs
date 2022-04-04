using CrashySmashy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CrashySmashy.Controllers
{
    public class HomeController : Controller
    {
        //constructor
        private CrashesContext _appContext { get; set; }
        public HomeController(CrashesContext crashesContext)
        {
            _appContext = crashesContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SeeTable()
        {
            var crashes = _appContext.Crashes
                
                
                .Take(10)
                .ToList();

            return View(crashes);
        }

        
    }
}
