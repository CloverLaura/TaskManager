using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.ViewModels
{
    public class IndexViewModel
    {
        [Required (ErrorMessage ="Invalid Email")]
        [EmailAddress]
        [Display (Name = "Email: ")]
        public string Email { get; set; }

        [Required (ErrorMessage ="Invaild Password")]
        [Display (Name ="Password: ")]
        public string Password { get; set; }

    }
}
