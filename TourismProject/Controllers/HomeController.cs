using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TourismProject.Models;

namespace TourismProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // You can replace this mock data with actual data from your database
            var model = new HomeViewModel
            {
                PopularDestinations = new List<string>
                {
                    "Sydney", "Gold Coast", "Melbourne"
                },
                PopularPackages = new List<TravelPackageViewModel>
                {
                    new TravelPackageViewModel
                    {
                        TravelPackageId = 1,
                        PackageName = "Bridge Climb",
                        Destination = "Sydney",
                        Price = 80,
                        DurationInDays = 3,
                        StartDate = DateTime.Today.AddDays(10),
                        AverageRating = 4.7,
                        ReviewCount = 24,
                        TravelAgencyName = "Explore Sydney",
                        AvailableSpots = 12
                    },
                    // Add more sample packages if needed
                },
                UpcomingPackages = new List<TravelPackageViewModel>
                {
                    new TravelPackageViewModel
                    {
                        TravelPackageId = 2,
                        PackageName = "Baguio Summer Chill",
                        Destination = "Baguio",
                        Price = 199.99m,
                        DurationInDays = 2,
                        StartDate = DateTime.Today.AddDays(5),
                        AverageRating = 4.3,
                        ReviewCount = 12,
                        TravelAgencyName = "Highland Tours",
                        AvailableSpots = 8
                    }
                },
                LatestReviews = new List<ReviewViewModel>
                {
                    new ReviewViewModel
                    {
                        PackageName = "Island Hopping Adventure",
                        TravelAgencyName = "Explore PH",
                        Rating = 5,
                        Comments = "Best trip ever!",
                        TouristName = "Jane Doe",
                        ReviewDate = DateTime.Today.AddDays(-2)
                    }
                }
            };



            // Example group details (you can replace with real user/group info)
            ViewBag.GroupDetails = new List<dynamic>
            {
                new { StudentId = "S001", FullName = "Alice Dela Cruz" },
                new { StudentId = "S002", FullName = "Bob Santos" },
                new { StudentId = "S003", FullName = "Carlos Reyes" }
            };

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Homepage()
        {
            return View();
        }
    }
}
