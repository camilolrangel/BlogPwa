﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWABlog.ViewModels.Admin;

namespace PWABlog.Controllers.Admin
{
    [Authorize]
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Painel()
        {
            AdminPainelViewModel model = new AdminPainelViewModel();

            return View(model);
        }
    }
}