using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TourismProject.Models;
using Microsoft.AspNet.Identity;


namespace TourismProject.Controllers
{
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();

            // Check if the user is in the Tourist role
            var isTourist = User.IsInRole("Tourist");

            if (isTourist)
            {
                // For Tourist users, show only their own bookings
                var tourist = db.Tourists.FirstOrDefault(t => t.UserId == currentUserId);

                if (tourist == null)
                {
                    return HttpNotFound("Tourist not found.");
                }

                var bookings = db.Bookings
                                 .Where(b => b.TouristId == tourist.TouristId) // Only bookings for the current tourist
                                 .Include(b => b.Tourist)
                                 .Include(b => b.TourPackage)
                                 .ToList();

                return View(bookings);
            }
            else if (User.IsInRole("Agency"))
            {
                // For Agency users, show all bookings
                var bookings = db.Bookings
                                 .Include(b => b.Tourist)
                                 .Include(b => b.TourPackage)
                                 .ToList();

                return View(bookings);
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Booking booking = db.Bookings
                                .Include(b => b.Tourist)
                                .Include(b => b.TourPackage)
                                .FirstOrDefault(b => b.BookingId == id);

            if (booking == null)
            {
                return HttpNotFound();
            }

            // Check if the current user is a Tourist and if the booking belongs to them
            if (User.IsInRole("Tourist"))
            {
                string currentUserId = User.Identity.GetUserId();
                var tourist = db.Tourists.FirstOrDefault(t => t.UserId == currentUserId);

                if (tourist == null || booking.TouristId != tourist.TouristId)
                {
                    return HttpNotFound("Booking not found for this user.");
                }
            }

            return View(booking);
        }




        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.TourPackageId = new SelectList(db.TourPackages, "TourPackageId", "Title");
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TourPackageId,BookingDate,PaymentCompleted")] Booking booking)
        {
            string currentUserId = User.Identity.GetUserId();

            // Find the logged-in tourist based on the current user's ID
            var tourist = db.Tourists.FirstOrDefault(t => t.UserId == currentUserId);
            if (tourist == null)
            {
                return HttpNotFound("Tourist not found.");
            }

            // Automatically set the TouristId based on the logged-in user
            booking.TouristId = tourist.TouristId;
            booking.Status = "Pending"; // Set default status to "Pending"

            db.Bookings.Add(booking);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.TouristId = new SelectList(db.Tourists, "TouristId", "UserId", booking.TouristId);
            ViewBag.TourPackageId = new SelectList(db.TourPackages, "TourPackageId", "Title", booking.TourPackageId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,TouristId,TourPackageId,BookingDate,Status,PaymentCompleted")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TouristId = new SelectList(db.Tourists, "TouristId", "UserId", booking.TouristId);
            ViewBag.TourPackageId = new SelectList(db.TourPackages, "TourPackageId", "Title", booking.TourPackageId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
