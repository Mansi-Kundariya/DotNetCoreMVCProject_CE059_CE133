using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripEventPlanner.Data;
using TripEventPlanner.Models;

namespace TripEventPlanner.Controllers
{
    public class SearchController : Controller
    {
        private readonly ItravelPlannerDBContext _context;

        public SearchController(ItravelPlannerDBContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //  List<Activity> ActivityList = _context.Activities.ToList();
        //return View(ActivityList);
        //}

        public IActionResult Index(string option, string search)
        {
            if (option == "Activity")
            {
                return View(_context.Activities
                    .Where(x => x.ActivityType.Name == search || search == null).ToList());
            }
            else if (option == "Location")
            {
                return View(_context.Activities
                    .Where(x => x.Location.Name == search || search == null).ToList());
            }
            else
            {
                return View(_context.Activities.Where(x => x.Name.StartsWith(search) || search == null).ToList());
            }
        }

        public IActionResult Details(int id)
        {
            return View();
        }
    }
}