using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TourismProject.Models;

namespace TourismProject.Controllers
{
    [Authorize]
    public class FeedbacksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Feedbacks
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            if (User.IsInRole("Tourist"))
            {
                var tourist = db.Tourists.FirstOrDefault(t => t.UserId == userId);
                if (tourist == null)
                {
                    TempData["ErrorMessage"] = "Tourist profile not found.";
                    return RedirectToAction("Index", "Home");
                }

                var feedbacks = db.Feedbacks
                    .Include(f => f.TourPackage)
                    .Where(f => f.TouristId == tourist.TouristId)
                    .ToList();

                return View(feedbacks);
            }
            else if (User.IsInRole("Agency"))
            {
                // Agencies see all feedbacks
                var feedbacks = db.Feedbacks
                    .Include(f => f.TourPackage)
                    .Include(f => f.Tourist)
                    .ToList();

                return View(feedbacks);
            }
            else
            {
                TempData["ErrorMessage"] = "Access denied. Only registered tourists or agencies can view feedbacks.";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Feedbacks/Create
        public ActionResult Create()
        {
            ViewBag.TourPackageId = new SelectList(db.TourPackages, "TourPackageId", "Title");
            return View();
        }

        // POST: Feedbacks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TourPackageId,Rating,Comment,SubmittedDate")] Feedback feedback)
        {
            string userId = User.Identity.GetUserId();
            var tourist = db.Tourists.FirstOrDefault(t => t.UserId == userId);

            if (tourist == null)
            {
                TempData["ErrorMessage"] = "Tourist profile not found.";
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                feedback.TouristId = tourist.TouristId;
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TourPackageId = new SelectList(db.TourPackages, "TourPackageId", "Title", feedback.TourPackageId);
            return View(feedback);
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Feedback feedback = db.Feedbacks
                .Include(f => f.TourPackage)
                .Include(f => f.Tourist)
                .FirstOrDefault(f => f.FeedbackId == id);

            if (feedback == null)
                return HttpNotFound();

            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
                return HttpNotFound();

            ViewBag.TourPackageId = new SelectList(db.TourPackages, "TourPackageId", "Title", feedback.TourPackageId);
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeedbackId,TourPackageId,TouristId,Rating,Comment,SubmittedDate")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TourPackageId = new SelectList(db.TourPackages, "TourPackageId", "Title", feedback.TourPackageId);
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Feedback feedback = db.Feedbacks
                .Include(f => f.TourPackage)
                .Include(f => f.Tourist)
                .FirstOrDefault(f => f.FeedbackId == id);

            if (feedback == null)
                return HttpNotFound();

            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(feedback);
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
