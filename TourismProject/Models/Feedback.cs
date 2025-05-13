using System;
using System.ComponentModel.DataAnnotations;

namespace TourismProject.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }

        [Required]
        public int TourPackageId { get; set; }

        [Required]
        public int TouristId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime SubmittedDate { get; set; }

        public virtual TourPackage TourPackage { get; set; }
        public virtual Tourist Tourist { get; set; }
    }
}
