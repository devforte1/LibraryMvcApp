
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
            if (ModelState.IsValid)
            {
                // RegistrantDAL db = new RegistrantDAL();
                UserOperations userOperations = new UserOperations();

                // Instantiate Registrant object from BLL.
                ViewBag.UserName = model.UserName;
                ViewBag.Password = model.Password;
                RegistrantDTO registrant = new RegistrantDTO(model.UserName, model.Password);

                bool result = userOperations.InsertUser(registrant);
                if (result)
                {
                    ViewBag.Message = $"Welcome, Registrant '{ViewBag.UserName}'.";
                }
                else
                {
                    ViewBag.Message = $"Unable to register '{ViewBag.UserName}'. Please contact the system administrator.";
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
