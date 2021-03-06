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
    public class RoleController : Controller
    { 

        private readonly ILogger<RoleController> _logger;

        public RoleController(ILogger<RoleController> logger)
        {
            _logger = logger;
        }

        [ValidateAntiForgeryToken]
        public IActionResult Index()
        {
            LibraryBLL.UserDTO userResult = LibraryCommon.SessionHelper.GetObjectFromJson<LibraryBLL.UserDTO>(HttpContext.Session, "user");
            if (userResult is not null)
            {
                ViewBag.CurrentUser = userResult;

                if (!(userResult.RoleName == "Administrator"))
                {
                    RedirectToAction("Index", "Home");
                }
            }

            RoleOperations operations = new RoleOperations();
            List<RoleDTO> roleDtoList = new List<RoleDTO>();
            List<RoleModel> roleModels = new List<RoleModel>();
            

            roleDtoList = operations.GetRoles();

            foreach(RoleDTO role in roleDtoList)
            {
                RoleModel model = new RoleModel();

                model.RoleId = role.RoleId;
                model.Name = role.Name;
                roleModels.Add(model);
            }
            ViewBag.Roles = roleModels;
            
            return View();
        }
    }
}
