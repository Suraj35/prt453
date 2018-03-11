using Microsoft.AspNet.Identity;
using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Users : IUser<long>, IDisposable
    {
        public long Id { get; set; }
                
        [Required(ErrorMessage = "User Name is required.")]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "Only alphanumeric and underscores are allowed in User Name field.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression("[A-Za-z ]*", ErrorMessage = "Please enter valid name.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Street Address")]
        public string Address { get; set; }
        
        public string TempPassword { get; set; }
        public bool IsPasswordResetRequested { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "Last Login")]
        public Nullable<DateTime> LastLogin { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date Created")]
        public System.DateTime DateCreated { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date Created")]
        public System.DateTime? DateModified { get; set; }
        [Display(Name = "Profile Picture")]
        public string ProfilePic { get; set; }
        [Display(Name="Team Registration")]
        [DefaultValue(false)]
        public bool IsTeam { get; set; }
        [Display(Name = "Total Members")]
        public int? TotalMembers { get; set; }
        public bool IsSuperAdmin { get; set; }
        
        public void Dispose()
        {

        }

    }
}