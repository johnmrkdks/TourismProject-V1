using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourismProject.Models
{
    public class TourPackage
    {
        public int TourPackageId { get; set; }

        [Required]
        public int AgencyId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public int DurationInDays { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int MaxGroupSize { get; set; }

        [Required]
        public DateTime AvailableDate { get; set; }

        public string ImageUrl { get; set; }

        public virtual Agency Agency { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
