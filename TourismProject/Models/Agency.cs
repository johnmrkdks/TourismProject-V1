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

        public virtual ICollection<TourPackage> TourPackages { get; set; }
    }
}
