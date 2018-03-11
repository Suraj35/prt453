
using Repository.Pattern.Ef6;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class Sports : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SportId { get; set; }
        [Display(Name = "Sport Name")]
        public string SportName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
    }
}