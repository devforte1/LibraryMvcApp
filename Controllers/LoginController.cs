using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using LibraryBLL;
using LibraryCommon;
using LibraryMvcApp.Models;

namespace LibraryMvcApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: LoginController/Login
        [HttpPost]
        // public IActionResult Login(string userName, string password)
        public IActionResult Index(UserModel model)
        {
            ViewBag.UserName = model.UserName;
            ViewBag.Password = model.Password;

            if (ModelState.IsValid)
            {
                UserOperations userOperations = new UserOperations();
                bool result = userOperations.LoginUser(ViewBag.UserName, ViewBag.Password);

                if (result)
                {
                    ViewBag.Message = $"Welcome, User '{ViewBag.UserName}'.";
                    return View();
                }
                else
                {
                    ViewBag.Message = $"Unable to authenticate '{ViewBag.UserName}'. Please enter valid credentials.";
                }
            }
            return View();
        }
    }
}
