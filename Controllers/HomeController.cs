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
            ViewBag.cities = _appContext.Crashes.Where(x => x.CITY != "***  ERROR  ***").Select(x => x.CITY).Distinct().OrderBy(x => x).ToList();
            ViewBag.counties = _appContext.Crashes.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x).ToList();
            return View(new Crash());
        }

        //returns the search view
        [HttpPost]
        public IActionResult Search(Crash crash)
        {
            var search = from s in _appContext.Crashes
                         select s;

            //these are all the possible searches that a user could do
            if (!String.IsNullOrEmpty(crash.COUNTY_NAME))
            {
                search = search
                    .Where(x => x.COUNTY_NAME.Contains(crash.COUNTY_NAME));
            }
            if (!String.IsNullOrEmpty(crash.CITY))
            {
                search = search
                    .Where(x => x.CITY.Contains(crash.CITY));
            }
            if (!String.IsNullOrEmpty(crash.MAIN_ROAD_NAME))
            {
                search = search
                    .Where(x => x.MAIN_ROAD_NAME.Contains(crash.MAIN_ROAD_NAME));
            }
            if (!String.IsNullOrEmpty(crash.ROUTE))
            {
                search = search
                    .Where(x => x.ROUTE.Contains(crash.ROUTE));
            }
            if (crash.MILEPOINT != null)
            {
                search = search
                    .Where(x => x.MILEPOINT == (crash.MILEPOINT));
            }
            if (crash.LAT_UTM_Y != null)
            {
                search = search
                    .Where(x => x.LAT_UTM_Y == (crash.LAT_UTM_Y));
            }
            if (crash.LONG_UTM_X != null)
            {
                search = search
                    .Where(x => x.LONG_UTM_X == (crash.LONG_UTM_X));
            }
            if (crash.CRASH_SEVERITY_ID != null)
            {
                search = search
                    .Where(x => x.CRASH_SEVERITY_ID == (crash.CRASH_SEVERITY_ID));
            }
            if (crash.WORK_ZONE_RELATED)
            {
                search = search
                    .Where(x => x.WORK_ZONE_RELATED == (crash.WORK_ZONE_RELATED));
            }
            if (crash.PEDESTRIAN_INVOLVED)
            {
                search = search
                    .Where(x => x.PEDESTRIAN_INVOLVED == (crash.PEDESTRIAN_INVOLVED ));
            }
            if (crash.BICYCLIST_INVOLVED)
            {
                search = search
                    .Where(x => x.BICYCLIST_INVOLVED == (crash.BICYCLIST_INVOLVED));
            }
            if (crash.MOTORCYCLE_INVOLVED)
            {
                search = search
                    .Where(x => x.MOTORCYCLE_INVOLVED == (crash.MOTORCYCLE_INVOLVED));
            }
            if (crash.IMPROPER_RESTRAINT)
            {
                search = search
                    .Where(x => x.IMPROPER_RESTRAINT == (crash.IMPROPER_RESTRAINT));
            }
            if (crash.UNRESTRAINED)
            {
                search = search
                    .Where(x => x.UNRESTRAINED == (crash.UNRESTRAINED));
            }
            if (crash.DUI)
            {
                search = search
                    .Where(x => x.DUI == (crash.DUI));
            }
            if (crash.INTERSECTION_RELATED)
            {
                search = search
                    .Where(x => x.INTERSECTION_RELATED == (crash.INTERSECTION_RELATED));
            }
            if (crash.WILD_ANIMAL_RELATED)
            {
                search = search
                    .Where(x => x.WILD_ANIMAL_RELATED == (crash.WILD_ANIMAL_RELATED));
            }
            if (crash.DOMESTIC_ANIMAL_RELATED)
            {
                search = search
                    .Where(x => x.DOMESTIC_ANIMAL_RELATED == (crash.DOMESTIC_ANIMAL_RELATED));
            }
            if (crash.OVERTURN_ROLLOVER)
            {
                search = search
                    .Where(x => x.OVERTURN_ROLLOVER == (crash.OVERTURN_ROLLOVER));
            }
            if (crash.COMMERCIAL_MOTOR_VEH_INVOLVED)
            {
                search = search
                    .Where(x => x.COMMERCIAL_MOTOR_VEH_INVOLVED == (crash.COMMERCIAL_MOTOR_VEH_INVOLVED));
            }
            if (crash.TEENAGE_DRIVER_INVOLVED)
            {
                search = search
                    .Where(x => x.TEENAGE_DRIVER_INVOLVED == (crash.TEENAGE_DRIVER_INVOLVED));
            }
            if (crash.OLDER_DRIVER_INVOLVED)
            {
                search = search
                    .Where(x => x.OLDER_DRIVER_INVOLVED == (crash.OLDER_DRIVER_INVOLVED));
            }
            if (crash.NIGHT_DARK_CONDITION)
            {
                search = search
                    .Where(x => x.NIGHT_DARK_CONDITION == (crash.NIGHT_DARK_CONDITION));
            }
            if (crash.SINGLE_VEHICLE)
            {
                search = search
                    .Where(x => x.SINGLE_VEHICLE == (crash.SINGLE_VEHICLE));
            }
            if (crash.DISTRACTED_DRIVING)
            {
                search = search
                    .Where(x => x.DISTRACTED_DRIVING == (crash.DISTRACTED_DRIVING));
            }
            if (crash.DROWSY_DRIVING)
            {
                search = search
                    .Where(x => x.DROWSY_DRIVING == (crash.DROWSY_DRIVING));
            }
            if (crash.ROADWAY_DEPARTURE)
            {
                search = search
                    .Where(x => x.ROADWAY_DEPARTURE == (crash.ROADWAY_DEPARTURE));
            }
           

            return View("SearchResults", search.Take(30).ToList());
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
