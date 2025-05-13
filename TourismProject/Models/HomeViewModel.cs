using System;
using System.Collections.Generic;

namespace TourismProject.Models
{
    public class HomeViewModel
    {
        public List<string> PopularDestinations { get; set; }
        public List<TravelPackageViewModel> PopularPackages { get; set; }
        public List<TravelPackageViewModel> UpcomingPackages { get; set; }
        public List<ReviewViewModel> LatestReviews { get; set; }
    }

    public class TravelPackageViewModel
    {
        public int TravelPackageId { get; set; }
        public string PackageName { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public DateTime StartDate { get; set; }
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
        public string TravelAgencyName { get; set; }
        public int AvailableSpots { get; set; }
    }

    public class ReviewViewModel
    {
        public string PackageName { get; set; }
        public string TravelAgencyName { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public string TouristName { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
