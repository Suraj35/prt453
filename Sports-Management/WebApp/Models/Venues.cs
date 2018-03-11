using Repository.Pattern.Ef6;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApp.Models
{
    public class Venues : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VenueId { get; set; }
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }
        [Display(Name = "Reserved")]
        public bool IsReserved { get; set; }
        public string Address { get; set; }
    }
}