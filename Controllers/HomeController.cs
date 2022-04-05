using CrashySmashy.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }


        // GET: /<controller>/
        [HttpGet]
        [Authorize]
        public IActionResult Admin(int page = 1, int? severity = null)
        {
            int pageSize = 10;
            ViewBag.currPage = page;
            var crashes = new List<Crash>();
            if (severity == null)
            {
                crashes = _appContext.Crashes
                   .OrderByDescending(x => x.CRASH_ID)
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();
                ViewBag.severity = null;
            }
            else
            {
                crashes = _appContext.Crashes
                   .Where(x => x.CRASH_SEVERITY_ID == severity)
                   .OrderByDescending(x => x.CRASH_ID)
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();
                ViewBag.severity = severity;
            }


            return View(crashes);
        }

        [HttpGet]
        public IActionResult AnalyticsMaster()
        {
            return View();
        }




        public IActionResult SeeTable(int page = 1, int? severity = null)
        {
            int pageSize = 10;
            ViewBag.currPage = page;
            var crashes = new List<Crash>();
            if (severity == null)
            {
                crashes = _appContext.Crashes
                   .OrderByDescending(x => x.CRASH_ID)
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();
                ViewBag.severity = null;
            }
            else
            {
                crashes = _appContext.Crashes
                   .Where(x => x.CRASH_SEVERITY_ID == severity)
                   .OrderByDescending(x => x.CRASH_ID)
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();
                ViewBag.severity = severity;
            }
           
            
            return View(crashes);
        }


        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.cities = _appContext.Crashes.Where(x => x.CITY != "***  ERROR  ***").Select(x => x.CITY).Distinct().OrderBy(x => x).ToList();
            ViewBag.counties = _appContext.Crashes.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x).ToList();
            return View(new Crash());
        }

        [HttpPost]
        [Authorize]
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
        [Authorize]
        public IActionResult Edit(int crashid)
        {
            ViewBag.cities = _appContext.Crashes.Where(x => x.CITY != "***  ERROR  ***").Select(x => x.CITY).Distinct().OrderBy(x => x).ToList();
            ViewBag.counties = _appContext.Crashes.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x).ToList();
            Crash crash = _appContext.Crashes.Single(x => x.CRASH_ID == crashid);
            return View("Create", crash);

        }


        [HttpPost]
        [Authorize]
        public IActionResult Edit(Crash crash)
        {
            _appContext.Update(crash);
            _appContext.SaveChanges();

            return RedirectToAction("SeeTable");
        }


        [Authorize]
        public IActionResult Delete(int crashid)
        {
            Crash crash = _appContext.Crashes.Single(x => x.CRASH_ID == crashid);
            _appContext.Remove(crash);
            _appContext.SaveChanges();
            return RedirectToAction("SeeTable");
        }

        public IActionResult CrashDetails(int crashid)
        {
            Crash crash = _appContext.Crashes.Single(x => x.CRASH_ID == crashid);
            return View(crash);
        }


    }


}
