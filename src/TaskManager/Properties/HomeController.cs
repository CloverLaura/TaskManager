﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Error()
        {
            return View();
        }
    }
}
