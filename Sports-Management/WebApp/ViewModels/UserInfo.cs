using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.ViewModels
{
    public class UserInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ProfilePic { get; set; }
        public bool IsTeam { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}