using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class EventsViewModels
    {
        public EventsViewModels()
        {
            SportsList = new HashSet<Sports>();
        }
        public int EventId { get; set; }
        [Display(Name = "Event Name")]
        [Required(ErrorMessage = "Event Name is required.")]


        public string EventName { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Event Start Date")]
        [Required(ErrorMessage = "Event Start Date is required.")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Event End Date")]
        [Required(ErrorMessage = "Event End Date is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Sport is required.")]
        public int SportId { get; set; }
        [Display(Name = "Sport Name")]
        public string SportName { get; set; }

        public long UserId { get; set; }
        public IEnumerable<Venues> VenueList { get; set; }
        [Required(ErrorMessage = "Venue is required.")]
        public int VenueId { get; set; }
        

        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }

        public IEnumerable<Sports> SportsList { get; set; }

        public bool IsActive { get; set; }
        [Display(Name = "Free Event")]
        [DefaultValue(true)]
        public bool IsFree { get; set; }
        [Display(Name = "Total Tickets")]
        public int? TotalTickets { get; set; }
        public int TotalBoughtTickets { get; set; }

        public bool IsTicketPurchased { get; set;}
    }

    public class ChallengeEvent
    {

        public int UserChallengeId { get; set; }
        public int EventId { get; set; }
        [Display(Name = "Event Name")]
        public string EventName { get; set; }
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Event Start Date")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Event End Date")]
        public DateTime EndDate { get; set; }

        public long UserId { get; set; }
        public long ToChallengeId { get; set; }
        public IEnumerable<Users> ToChallengeList { get; set; }
        public IEnumerable<ToChallenge> ToSelectedChallengesList { get; set; }
        public long[] SelectedIds { get; set; }
    }


    public class ToChallenge
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}