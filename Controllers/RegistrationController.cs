
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
    public class RegistrationController : Controller
    {
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(ILogger<RegistrationController> logger)
        {
            _logger = logger;
        }
        
        // GET: RegistrationController
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: RegistrationController
        [HttpPost]
        public IActionResult Index(RegistrantModel model)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated")))
            {
                HttpContext.Session.SetString("IsAuthenticated", "false");
            }

            if (ModelState.IsValid)
            {
                // RegistrantDAL db = new RegistrantDAL();
                UserOperations userOperations = new UserOperations();

                // Instantiate Registrant object from BLL.
                RegistrantDTO registrant = new RegistrantDTO(model.UserName, model.Password, model.FirstName, model.LastName, model.Email);

                bool result = userOperations.InsertUser(registrant);
                if (result)
                {
                    UserDTO user = userOperations.GetUserByUserName(model.UserName);
                    
                    // userOperations.SetUserRole(user.UserId,(int)_role.Patron);
                    ViewBag.Message = $"Welcome, Registrant '{model.UserName}'. Please log in to access your dashboard.";
                }
                else
                {
                    ViewBag.Message = $"Unable to register '{model.UserName}'. Please contact the system administrator.";
                }

                return View();
                // return RedirectToPage("/Register");
            }
            else
            {
                return View();
            }
        }
    }
}
