using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.ViewModels
{
    public class SignUpTaskViewModel
    {
        [Required]
        [Display (Name = "First Name: ")]
        public string FirstName { get; set; }

        [Required]
        [Display (Name ="Last Name: ")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display (Name ="Email: ")]
        public string Email { get; set; }

        [Phone]
        [Display (Name ="Phone Number: ")]
        public object PhoneNumber { get; set; }

        [Required]
        [Display (Name = "Password: ")]
        public string Password { get; set; }

        [Required]
        [Display (Name = "Verify Password: ")]
        public string Verify { get; set; }
    }
}
