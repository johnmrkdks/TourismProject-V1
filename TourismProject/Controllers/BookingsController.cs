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

            if (User.IsInRole("Tourist"))
            {
                var tourist = db.Tourists.FirstOrDefault(t => t.UserId == currentUserId);
                if (tourist == null)
                {
                    return HttpNotFound("Tourist not found.");
                }

                var bookings = db.Bookings
                                 .Where(b => b.TouristId == tourist.TouristId)
                                 .Include(b => b.Tourist)
                                 .Include(b => b.TourPackage)
                                 .ToList();

                return View(bookings);
            }
            else if (User.IsInRole("Agency"))
            {
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
        public ActionResult Create(int? id)
        {
            var booking = new Booking();

            if (id.HasValue)
            {
                booking.TourPackageId = id.Value;

                var selectedPackage = db.TourPackages.Find(id.Value);
                if (selectedPackage != null)
                {
                    ViewBag.PackageTitle = selectedPackage.Title;
                }
            }

            ViewBag.TourPackageId = new SelectList(db.TourPackages, "TourPackageId", "Title", booking.TourPackageId);
            return View(booking);
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TourPackageId,BookingDate,PaymentCompleted")] Booking booking)
        {
            string currentUserId = User.Identity.GetUserId();
            var tourist = db.Tourists.FirstOrDefault(t => t.UserId == currentUserId);

            if (tourist == null)
            {
                return HttpNotFound("Tourist not found.");
            }

            booking.TouristId = tourist.TouristId;
            booking.Status = "Pending";

            db.Bookings.Add(booking);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Booking booking = db.Bookings.Find(id);
            if (booking == null)
                return HttpNotFound();

            ViewBag.TouristId = new SelectList(db.Tourists, "TouristId", "UserId", booking.TouristId);
            ViewBag.TourPackageId = new SelectList(db.TourPackages, "TourPackageId", "Title", booking.TourPackageId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Booking booking = db.Bookings.Find(id);
            if (booking == null)
                return HttpNotFound();

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
