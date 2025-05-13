using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourismProject.Models
{
    public class AgencyDashboardViewModel
    {
        public int TotalBookings { get; set; }
        public int CompletedBookings { get; set; }
        public int PendingBookings { get; set; }
        public int TotalTourists { get; set; }
        public string MostPopularTourPackage { get; set; }

        public Dictionary<string, int> BookingStatusChart { get; set; }
        public Dictionary<string, int> PopularPackagesChart { get; set; }
    }

}