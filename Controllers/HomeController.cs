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
        //creates an _appcontext variable
        private CrashesContext _appContext { get; set; }
        
        
        
        
        //initialize the _appcontext, and set it equal to what is passed in.
        public HomeController(CrashesContext crashesContext)
        {
            _appContext = crashesContext;
        }

        
        //returns the index view
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }




        //returns the privacy view
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }


        //returns the privacy Unsubscribe view
        [HttpGet]
        public IActionResult PrivacyUnsubscribe()
        {
            return View();
        }


        //returns the search view
        [HttpGet]
        public IActionResult Search()
        {
            
            return View(new Crash());
        }

        //returns the search view
        [HttpPost]
        public IActionResult Search(Crash crash)
        {
            var search = from s in _appContext.Crashes
                         select s;

            if (!String.IsNullOrEmpty(crash.COUNTY_NAME))
            {
                search = search.Where(x => x.COUNTY_NAME.Contains(crash.COUNTY_NAME));
            }
            return View("SearchResults", search.ToList());
        }

        //returns the search view
        [HttpGet]
        public IActionResult SearchResults(string searching)
        {
            var search = from s in _appContext.Crashes
                         select s;

            if (!String.IsNullOrEmpty(searching))
            {
                search = search.Where(x => x.COUNTY_NAME.Contains(searching));
            }
            return View(search.ToList());
        }



        //This returns the crashes page that shows all of the crashes. To be able to see all of the different pieces, you need to be logged in as an admin,
        //while the other view does not allow for edit and delete.
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





        //This returns the crashes page that shows all of the crashes. To be able to see all of the different pieces, you need to be logged in as an admin,
        //while the other view does not allow for edit and delete.
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




        //This is where you insert the information to report a new crash. 
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.cities = _appContext.Crashes.Where(x => x.CITY != "***  ERROR  ***").Select(x => x.CITY).Distinct().OrderBy(x => x).ToList();
            ViewBag.counties = _appContext.Crashes.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x).ToList();
            return View(new Crash());
        }




        //This is where you submit the information to report a new crash. It is then stored to the database.
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



        //This is where you are routed to an edit page where you can edit a file.
        [HttpGet]
        [Authorize]
        public IActionResult Edit(int crashid)
        {
            ViewBag.cities = _appContext.Crashes.Where(x => x.CITY != "***  ERROR  ***").Select(x => x.CITY).Distinct().OrderBy(x => x).ToList();
            ViewBag.counties = _appContext.Crashes.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x).ToList();
            Crash crash = _appContext.Crashes.Single(x => x.CRASH_ID == crashid);
            return View("Create", crash);

        }




        //This is where you can submit the edits, and then resave it to the database.
        [HttpPost]
        [Authorize]
        public IActionResult Edit(Crash crash)
        {
            _appContext.Update(crash);
            _appContext.SaveChanges();

            return RedirectToAction("SeeTable");
        }



        //This is where you can delete a file from a database, but it only works if you are authorized.
        [Authorize]
        public IActionResult Delete(int crashid)
        {
            Crash crash = _appContext.Crashes.Single(x => x.CRASH_ID == crashid);
            _appContext.Remove(crash);
            _appContext.SaveChanges();
            return RedirectToAction("SeeTable");
        }



        //This view will return the details of a single crash.
        public IActionResult CrashDetails(int crashid)
        {
            Crash crash = _appContext.Crashes.Single(x => x.CRASH_ID == crashid);
            return View(crash);
        }


    }


}
