using System;
using System.ComponentModel.DataAnnotations;

namespace TourismProject.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public int TouristId { get; set; }

        [Required]
        public int TourPackageId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public string Status { get; set; } // e.g., Pending, Confirmed, Completed

        public bool PaymentCompleted { get; set; }

        public virtual Tourist Tourist { get; set; }
        public virtual TourPackage TourPackage { get; set; }
    }
}
