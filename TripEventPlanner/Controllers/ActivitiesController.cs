using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TripEventPlanner.Data;
using TripEventPlanner.Models;
using Microsoft.AspNetCore.Http;

namespace TripEventPlanner.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly ItravelPlannerDBContext _context;

        public ActivitiesController(ItravelPlannerDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string activityType, string location)
        {
            if (location == null)
            {
                location = "All";
            }
            if (activityType == null)
            {
                activityType = "All";
            }
            ViewData["CurrentFilter"] = searchString;

            var activityTypeData = _context.ActivityTypes;
            ViewData["activityTypeFilter"] = activityTypeData;
            ViewData["activityType"] = activityType;

            var locationData = _context.Locations;
            ViewData["locationFilter"] = locationData;
            ViewData["location"] = location;

            var stringEmty = !String.IsNullOrEmpty(searchString);

            var queryType = _context.Activities
                   .Include(a => a.ActivityType).Where(s => activityType == s.ActivityType.Name)
                   .Include(a => a.Location)
                   .Include(s => s.ActivityType);
            var queryLocation = _context.Activities
                   .Include(a => a.ActivityType)
                   .Include(a => a.Location).Where(s => location == s.Location.Name)
                   .Include(s => s.ActivityType);
            var queryAll = _context.Activities
                    .Include(a => a.ActivityType).Where(s => activityType == s.ActivityType.Name)
                   .Include(a => a.Location).Where(s => location == s.Location.Name)
                   .Include(s => s.ActivityType);
            var activity = _context.Activities
                .Include(a => a.ActivityType)
                .Include(a => a.Location)
                .Include(s => s.ActivityType);

            if (String.IsNullOrEmpty(searchString) & activityType != "All" & location != "All")
            {
                activity = queryAll;
            }
            if (String.IsNullOrEmpty(searchString) & activityType == "All" & location != "All")
            {
                activity = queryLocation;
            }
            if (String.IsNullOrEmpty(searchString) & activityType != "All" & location == "All")
            {
                activity = queryType;
            }
            if (activityType == "All" & location == "All" & stringEmty)
            {
                activity = _context.Activities
                    .Where(s => s.Name.Contains(searchString))
                    .Include(a => a.Location)
                    .Include(s => s.ActivityType);
            }

            if (activityType != "All" & location != "All" & stringEmty)
            {
                activity = _context.Activities
                    .Where(s => s.Name.Contains(searchString) && s.ActivityType.Name.Contains(activityType))
                    .Include(a => a.Location).Where(s => location == s.Location.Name)
                    .Include(s => s.ActivityType);
            }
            if (activityType != "All" & location == "All" & stringEmty)
            {
                activity = _context.Activities
                    .Where(s => s.Name.Contains(searchString) && s.ActivityType.Name.Contains(activityType))
                    .Include(a => a.Location)
                    .Include(s => s.ActivityType);
            }
            if (activityType == "All" & location != "All" & stringEmty)
            {
                activity = _context.Activities
                    .Where(s => s.Name.Contains(searchString) && s.Location.Name.Contains(location))
                    .Include(a => a.Location)
                    .Include(s => s.ActivityType);
            }

            return View(await activity.AsNoTracking().ToListAsync());
        }

        // GET: Activities/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .Include(a => a.ActivityType)
                .Include(a => a.Location)
                .FirstOrDefaultAsync(m => m.ActivityId == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // GET: Activities/Create
        public IActionResult Create()
        {
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "ActivityTypeId", "Name");
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create2(string ActivityTypeId, string Name, string Description,
            string Address, string Price, string ImageUrl, string LocationId)
        {
            if (ModelState.IsValid)
            {
                _context.Activities.Add(new Activity
                {
                    ActivityTypeId = 1,
                    Name = Name,
                    Description = Description,
                    Address = Address,
                    Price = Convert.ToInt32(Price),
                    ImageUrl = ImageUrl,
                    //StartDate = DateTime.Parse(StartDate),
                    //EndDate = DateTime.Parse(EndDate) ,
                    LocationId = Convert.ToInt16(LocationId)
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ActivityId,Name,Description,Address,Price,ActivityTypeId,ImageUrl,StartDate,EndDate,LocationId")]
        Activity activity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "ActivityTypeId", "Name", activity.ActivityTypeId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", activity.LocationId);
            return View(activity);
        }

        // GET: Activities/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "ActivityTypeId", "Name", activity.ActivityTypeId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", activity.LocationId);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("ActivityId,Name,Description,Address,Price,ActivityTypeId,ImageUrl,StartDate,EndDate,LocationId")] Activity activity)
        {
            if (id != activity.ActivityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(activity.ActivityId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "ActivityTypeId", "Name", activity.ActivityTypeId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", activity.LocationId);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .Include(a => a.ActivityType)
                .Include(a => a.Location)
                .FirstOrDefaultAsync(m => m.ActivityId == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var activity = await _context.Activities.FindAsync(id);
            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityExists(short id)
        {
            return _context.Activities.Any(e => e.ActivityId == id);
        }
    }
}