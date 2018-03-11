using Repository.Pattern.Ef6;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApp.Models
{
    public class UserChallenges : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserChallengeId { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name ="Challenge Accepted")]
        public bool IsAccepted { get; set; }


        [Display(Name = "Challenger")]
        public long? UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }
        [Display(Name = "To Challenge")]
        public long? ToChallengeId { get; set; }
        [ForeignKey("ToChallengeId")]
        public Users ToChallenge { get; set; }

        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Events Event { get; set; }
    }
}