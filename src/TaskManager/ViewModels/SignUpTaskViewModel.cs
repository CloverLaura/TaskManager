using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class SignUpTaskViewModel
    {
        [Required (ErrorMessage ="You must enter a first name")]
        [Display (Name = "First Name: ")]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "You must enter a last name")]
        [Display (Name ="Last Name: ")]
        public string LastName { get; set; }

        [Required (ErrorMessage= "You must enter a vaild email address")]
        [EmailAddress]
        [Display (Name ="Email: ")]
        public string Email { get; set; }

        [Phone]
        [Display (Name ="Phone Number: ")]
        public object PhoneNumber { get; set; }

        [Required (ErrorMessage= "You must enter a valid password")]
        [Display (Name = "Password: ")]
        public string Password { get; set; }

        [Required (ErrorMessage = "Passwords must match")]
        [Display (Name = "Verify Password: ")]
        [Compare("Password")]
        public string Verify { get; set; }

        [Display (Name = "Select your team(s):")]
        public List<Team> Teams { get; set; }

        TeamData teamData = new TeamData();

        public SignUpTaskViewModel()
        {
            this.Teams = new List<Team>();

            
        }

        
        //public List<Team> Teams { get; set; }

        public class Team
        {
            public bool IsSelected { get; set; }
            public string Name { get; set; }
            //public int TeamID { get; set; }
        }
    }
}
