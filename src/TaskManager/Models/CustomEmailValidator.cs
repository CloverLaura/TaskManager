using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class CustomEmailValidator : ValidationAttribute 
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string email = value.ToString();

                if (Regex.IsMatch(email, @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", RegexOptions.IgnoreCase))
                {
                    UserData userData = new UserData();
                    List<User> users = userData.AllUsersToList();
                    foreach(User user in users)
                    {
                        if(user.Email == email)
                        {
                            return ValidationResult.Success;
                        }
                    }
                    return new ValidationResult("Email not found, please sign up.");
                }
                else
                {
                    return new ValidationResult("Please enter valid email address");
                }
            }
            
            return new ValidationResult("" + validationContext.DisplayName + " is required");
        }
    }
}
