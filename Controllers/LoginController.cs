using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

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
            // If the user already has a current authenticated session then redirect to the landing page.
            LibraryBLL.UserDTO userResult = LibraryCommon.SessionHelper.GetObjectFromJson<LibraryBLL.UserDTO>(HttpContext.Session, "user");
            if (userResult is not null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: LoginController/Index
        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            // If the user already has a current authenticated session then redirect to the landing page.
            LibraryBLL.UserDTO userResult = LibraryCommon.SessionHelper.GetObjectFromJson<LibraryBLL.UserDTO>(HttpContext.Session, "user");
            if(userResult is not null)
            {
                return RedirectToAction("Index","Home");
            }

            if (ModelState.IsValid)
            {
                UserOperations userOperations = new UserOperations();
                bool result = userOperations.LoginUser(model.UserName, model.Password);

                if (result)
                {
                    // Set up the user session profile here.
                    UserOperations db = new UserOperations();
                    UserDTO user = db.GetUserByUserName(model.UserName);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "user", user);
                    HttpContext.Session.SetString("IsAuthenticated", "true");

                    ViewBag.Message = $"Welcome, {user.RoleName} '{model.UserName}'.";
                }
                else
                {
                    ViewBag.Message = $"Unable to authenticate '{model.UserName}'. Please enter valid credentials.";
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();

            AuthenticationHttpContextExtensions.SignOutAsync(HttpContext, "Cookies");

            AuthenticationHttpContextExtensions
            .SignOutAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index","Home");
        }
    }
}
