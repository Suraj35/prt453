using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Notifications : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotificationId { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public Users Users { get; set; }
        public string Notification { get; set; }
        public string Link { get; set; }
        public bool IsRead { get; set; }
        public DateTime NotificationDate { get; set; }
        public string Icon { get; set; }
        public string ProfilePic { get; set; }
    }
}