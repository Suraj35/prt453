using Repository.Pattern.Ef6;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class Tickets : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketId { get; set; }
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Events Event { get; set; }
        public long? UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
