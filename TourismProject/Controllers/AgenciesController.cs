using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TourismProject.Models;

namespace TourismProject.Controllers
{
    public class AgenciesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        [Authorize(Roles = "Agency")]
        public ActionResult Dashboard()
        {
            var totalBookings = db.Bookings.Count();
            var completedBookings = db.Bookings.Count(b => b.Status == "Completed");
            var pendingBookings = db.Bookings.Count(b => b.Status == "Pending");
            var totalTourists = db.Tourists.Count();

            // Most popular tour package
            var mostPopularPackage = db.Bookings
                .GroupBy(b => b.TourPackage.Title)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault() ?? "N/A";

            // Booking Status Chart Data
            var bookingsByStatus = db.Bookings
                .GroupBy(b => b.Status)
                .ToDictionary(g => g.Key, g => g.Count());

            // Top 5 Popular Tour Packages Chart Data
            var popularPackages = db.Bookings
                .Where(b => b.TourPackage != null)
                .GroupBy(b => b.TourPackage.Title)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .ToDictionary(g => g.Key, g => g.Count());

            var model = new AgencyDashboardViewModel
            {
                TotalBookings = totalBookings,
                CompletedBookings = completedBookings,
                PendingBookings = pendingBookings,
                TotalTourists = totalTourists,
                MostPopularTourPackage = mostPopularPackage,
                BookingStatusChart = bookingsByStatus,
                PopularPackagesChart = popularPackages
            };

            return View(model);
        }



        // GET: Agencies
        public ActionResult Index()
        {
            return View(db.Agencies.ToList());
        }

        // GET: Agencies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agency agency = db.Agencies.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // GET: Agencies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AgencyId,UserId,AgencyName,Description,ServicesOffered,ImageUrl")] Agency agency)
        {
            if (ModelState.IsValid)
            {
                db.Agencies.Add(agency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agency);
        }

        // GET: Agencies/Edit/5
        public ActionResult Edit(int id)
        {
            // Fetch the tour package from the database
            var tourPackage = db.TourPackages.Include(t => t.Agency).FirstOrDefault(t => t.TourPackageId == id);

            if (tourPackage == null)
            {
                return HttpNotFound();
            }

            // Populate the list of agencies for the dropdown
            ViewBag.AgencyId = new SelectList(db.Agencies, "AgencyId", "AgencyName", tourPackage.AgencyId);

            return View(tourPackage);
        }


        // POST: Agencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AgencyId,UserId,AgencyName,Description,ServicesOffered,ImageUrl")] Agency agency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agency);
        }

        // GET: Agencies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agency agency = db.Agencies.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // POST: Agencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agency agency = db.Agencies.Find(id);
            db.Agencies.Remove(agency);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
