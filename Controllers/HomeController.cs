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
                .OrderByDescending(x => x.CRASH_ID)
                .Take(10)
                .ToList();

            return View(crashes);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.cities = _appContext.Crashes.Where(x => x.CITY != "***  ERROR  ***").Select(x => x.CITY).Distinct().OrderBy(x => x).ToList();
            ViewBag.counties = _appContext.Crashes.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x).ToList();
            return View(new Crash());
        }

        [HttpPost]
        public IActionResult Create(Crash crash)
        {
            if (ModelState.IsValid)
            {
                _appContext.Crashes.Add(crash);
                _appContext.SaveChanges();
                return RedirectToAction("SeeTable");
            }
            ViewBag.cities = _appContext.Crashes.Where(x => x.CITY != "***  ERROR  ***").Select(x => x.CITY).Distinct().OrderBy(x => x).ToList();
            ViewBag.counties = _appContext.Crashes.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x).ToList();
            return View("Create");
        }

        [HttpGet]
        public IActionResult Edit(int crashid)
        {
            ViewBag.cities = _appContext.Crashes.Where(x => x.CITY != "***  ERROR  ***").Select(x => x.CITY).Distinct().OrderBy(x => x).ToList();
            ViewBag.counties = _appContext.Crashes.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x).ToList();
            Crash crash = _appContext.Crashes.Single(x => x.CRASH_ID == crashid);
            return View("Create", crash);

        }
        [HttpPost]
        public IActionResult Edit(Crash crash)
        {
            _appContext.Update(crash);
            _appContext.SaveChanges();

            return RedirectToAction("SeeTable");
        }
        
        public IActionResult Delete(int crashid)
        {
            Crash crash = _appContext.Crashes.Single(x => x.CRASH_ID == crashid);
            _appContext.Remove(crash);
            _appContext.SaveChanges();
            return RedirectToAction("SeeTable");
        }

    }


}
