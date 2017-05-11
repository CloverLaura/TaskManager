using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Controllers
{

    public class LoginController : Controller
    {

        //private static UserData userData;


        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index(string email)
        {
            UserData userData = new UserData();
            User user = userData.GetByEmail(email);

            return RedirectToAction("Home", new { id = user.UserID });
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            SignUpTaskViewModel signUpTaskViewModel = new SignUpTaskViewModel();
            return View(signUpTaskViewModel);
        }

        [HttpPost]
        public IActionResult SignUp(SignUpTaskViewModel signUpTaskViewModel)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User
                {
                    FirstName = signUpTaskViewModel.FirstName,
                    LastName = signUpTaskViewModel.LastName,
                    Email = signUpTaskViewModel.Email,
                    Phone = signUpTaskViewModel.PhoneNumber,
                    Password = signUpTaskViewModel.Password
                };

                UserData userData = new UserData();
                userData.Add(newUser);

                return RedirectToAction("Home", new { id = newUser.UserID } );
            }

            else
            {
                return View(signUpTaskViewModel);
            }
        }

        public IActionResult Home(int id)
        {
            UserData userData = new UserData();
            return View(userData.GetById(id));
        }
    }

    
}
