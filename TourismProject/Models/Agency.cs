using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourismProject.Models
{
    public class Agency
    {
        public int AgencyId { get; set; }

        [Required]
        public string UserId { get; set; } // Linked to ASP.NET Identity User

        [Required]
        [StringLength(150)]
        public string AgencyName { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public string ServicesOffered { get; set; }

        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        public virtual ICollection<TourPackage> TourPackages { get; set; }
    }
}
