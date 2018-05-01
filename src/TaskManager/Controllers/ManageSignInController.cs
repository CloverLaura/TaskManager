using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Controllers
{
    public class ManageSignInController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            
            return View();
        }

        [AcceptVerbs]
        [HttpPost]
        public ActionResult Index(SignInModel model)
        {
            UserData userData = new UserData();
            User user = userData.GetByEmail(model.Email);

            if (ModelState.IsValid)
            {
                if(model.Password == null)
                {
                    ModelState.AddModelError("Password", "You must enter your password");
                }
                if(user.Password == model.Password)
                {
                    Response.Cookies.Append("userCookie", user.UserID.ToString());
                    return RedirectToAction("Home", "Login");
                }
                else
                {
                    ModelState.AddModelError("Password", "Incorrect Password");
                }
            }
            return View(model);

        }
            
    }
}
