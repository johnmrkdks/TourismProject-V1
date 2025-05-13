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
    public class TourPackagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TourPackages
        public ActionResult Index()
        {
            var tourPackages = db.TourPackages.Include(t => t.Agency).ToList();
            return View(tourPackages);
        }


        // GET: TourPackages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourPackage tourPackage = db.TourPackages.Find(id);
            if (tourPackage == null)
            {
                return HttpNotFound();
            }
            return View(tourPackage);
        }

        // GET: TourPackages/Create
        public ActionResult Create()
        {
            ViewBag.AgencyId = new SelectList(db.Agencies, "AgencyId", "UserId");
            return View();
        }

        // POST: TourPackages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TourPackageId,AgencyId,Title,Description,DurationInDays,Price,MaxGroupSize,AvailableDate,ImageUrl")] TourPackage tourPackage)
        {
            if (ModelState.IsValid)
            {
                db.TourPackages.Add(tourPackage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgencyId = new SelectList(db.Agencies, "AgencyId", "UserId", tourPackage.AgencyId);
            return View(tourPackage);
        }

        // GET: TourPackages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!User.IsInRole("Agency"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourPackage tourPackage = db.TourPackages.Find(id); // Find the TourPackage, not Booking
            if (tourPackage == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgencyId = new SelectList(db.Agencies, "AgencyId", "AgencyName", tourPackage.AgencyId); // Use AgencyName instead of UserId
            return View(tourPackage);
        }

        // POST: TourPackages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TourPackageId,AgencyId,Title,Description,DurationInDays,Price,MaxGroupSize,AvailableDate,ImageUrl")] TourPackage tourPackage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tourPackage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgencyId = new SelectList(db.Agencies, "AgencyId", "AgencyName", tourPackage.AgencyId); // Use AgencyName here as well
            return View(tourPackage);
        }


        // GET: TourPackages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourPackage tourPackage = db.TourPackages.Find(id);
            if (tourPackage == null)
            {
                return HttpNotFound();
            }
            return View(tourPackage);
        }

        // POST: TourPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TourPackage tourPackage = db.TourPackages.Find(id);
            db.TourPackages.Remove(tourPackage);
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
