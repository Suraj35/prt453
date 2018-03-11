using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class Events : Entity
    {
        public Events()
        {
            Ticket = new HashSet<Tickets>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }
        [Display(Name = "Event Name")]
        public string EventName { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Event Start Date")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Event End Date")]
        public DateTime EndDate { get; set; }


        public int SportId { get; set; }
        [ForeignKey("SportId")]
        public Sports Sport { get; set; }

        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }
        [ForeignKey("VenueId")]
        public Venues Venue { get; set; }
        public int VenueId { get; set; }

        [Display(Name = "Free Event")]
        [DefaultValue(true)]
        public bool IsFree { get; set; }
        [Display(Name = "Total Tickets")]
        public int? TotalTickets { get; set; }


        public virtual ICollection<Tickets> Ticket { get; set; }
    }
}