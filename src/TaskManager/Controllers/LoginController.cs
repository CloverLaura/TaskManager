using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;
using static TaskManager.ViewModels.SignUpTaskViewModel;
using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Internal.Networking;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Controllers
{

    public class LoginController : Controller
    {



        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel indexViewModel)
        {
            UserData userdata = new UserData();
            //string password = user.Password;
            if (ModelState.IsValid & userdata.ValidateEmail(indexViewModel.Email))
                {
                    UserData userData = new UserData();
                    User user = userData.GetByEmail(indexViewModel.Email);
                    Response.Cookies.Append("userCookie", user.UserID.ToString());
                    return RedirectToAction("Home", new { email = indexViewModel.Email });
                }
                else
                {
                    return View(indexViewModel);
                }
            
            
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
            UserData userdata = new UserData();
            if (userdata.IsValidUsername(signUpTaskViewModel.Username))
            {
                if (ModelState.IsValid & userdata.IsValidPhone(signUpTaskViewModel.PhoneNumber))
                {

                User newUser = new User
                {
                    Username = signUpTaskViewModel.Username,
                    FirstName = signUpTaskViewModel.FirstName,
                    LastName = signUpTaskViewModel.LastName,
                    Email = signUpTaskViewModel.Email,
                    PhoneNumber = signUpTaskViewModel.PhoneNumber,
                    Password = signUpTaskViewModel.Password,
                };

                UserData userData = new UserData();
                userData.Add(newUser);
                User user = userData.GetByEmail(signUpTaskViewModel.Email);
                Response.Cookies.Append("userCookie", user.UserID.ToString());

                return RedirectToAction("Home", new { email = signUpTaskViewModel.Email });

                }
                else
                {
                    ModelState.AddModelError("PhoneNumber", "You must enter a vaild phone number");
                    return View(signUpTaskViewModel);
                }
            }

            else
            {
                ModelState.AddModelError("Username", "Username already taken");
                return View(signUpTaskViewModel);
            }
        }

        public IActionResult AddTeam()
        {
            return View(new AddTeamViewModel());
        }

        [HttpPost]
        public IActionResult AddTeam(AddTeamViewModel addTeamViewModel, string submit)
        {
            UserData userData = new UserData();
            TeamData teamData = new TeamData();
            if (ModelState.IsValid)
            {
                User user = userData.GetById(Convert.ToInt32(HttpContext.Request.Cookies["userCookie"]));
                Team newTeam = new Team
                {
                    Name = addTeamViewModel.Name,
                    Description = addTeamViewModel.Description,
                    CreatedBy = user.UserID

                };
                teamData.Add(newTeam);
                userData.AddTeam(user, newTeam.Name);
                return RedirectToAction("Home", new { email = user.Email });
            }
            else
            {
                return View(addTeamViewModel);
            }
        }

        public IActionResult Home(string email)
        {
            UserData userData = new UserData();

            return View(userData.GetByEmail(email));
            
        }
    }

    
}
