﻿using System;
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
        public IActionResult Index(IndexViewModel indexViewModel)
        {

            if (ModelState.IsValid)
            { 
            UserData userData = new UserData();
            User user = userData.GetByEmail(indexViewModel.Email);

            return RedirectToAction("Home", new { id = user.UserID });
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
            TeamData teamData = new TeamData();
            foreach(var team in teamData.TeamsToList())
            {
                var newTeam = new SignUpTaskViewModel.Team
                {
                    Name = team.Name.ToString(),
                    //TeamID = team.TeamID
                };

                signUpTaskViewModel.Teams.Add(newTeam);
            }

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
                    Password = signUpTaskViewModel.Password,
                };

                for (var i = 0; i <= signUpTaskViewModel.Teams.Count(); i++)
                {
                    string teamName = signUpTaskViewModel.Teams[i].Name.ToString();
                    UserData data = new UserData();
                    TeamData teamData = new TeamData();
                    data.AddTeam(newUser, teamName);


                }

                UserData userData = new UserData();
                userData.Add(newUser);

                return RedirectToAction("Home", new { id = newUser.UserID } );
            }

            else
            {
                
                TeamData teamData = new TeamData();
                foreach (var team in teamData.TeamsToList())
                {
                    var newTeam = new SignUpTaskViewModel.Team
                    {
                        Name = team.Name.ToString(),
                        //TeamID = team.TeamID
                    };

                    signUpTaskViewModel.Teams.Add(newTeam);
                }

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
