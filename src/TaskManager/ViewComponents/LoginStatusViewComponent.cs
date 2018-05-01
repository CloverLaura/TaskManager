using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaskManager.Models;

namespace TaskManager.ViewComponents
{
    public class LoginStatusViewComponent : ViewComponent
    {
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            UserData userData = new UserData();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            User user = userData.GetById(userID);

            
            if (user == null)
            {
                return View();
                
                
            }
            if (user.LoggedOn)
            {
                return View("LoggedIn");

            }
            else
            {
                return View();
            }
            
            
            
        }

        
    }
}
