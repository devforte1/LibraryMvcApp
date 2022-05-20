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
    
    public class UserController : Controller
    {
        // GET: UserController
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult GetUsers()
        {
            LibraryBLL.UserDTO userResult = LibraryCommon.SessionHelper.GetObjectFromJson<LibraryBLL.UserDTO>(HttpContext.Session, "user");
            // if (userResult is not null)
            ViewBag.IsAuthenticated = "false";
            if (ViewBag.IsAuthenticated == "false")
            {
                RedirectToAction("Index", "Home");
            }

            UserOperations userOperations = new UserOperations();
            List<UserDTO> userDtoList = new List<UserDTO>();
            List<UserModel> userModels = new List<UserModel>();

            userDtoList = userOperations.GetUsers();

            ModelState.Clear();

            foreach (UserDTO item in userDtoList)
            {
                UserModel model = new UserModel();

                model.UserId = item.UserId;
                model.UserName = item.UserName;
                model.IsActive = item.IsActive;
                model.RoleName = item.RoleName;
                model.FirstName = item.FirstName;
                model.LastName = item.LastName;
                model.Email = item.Email;
                userModels.Add(model);
            }

            return View(userModels);
        }

        // GET: UserController
        [HttpGet]
        // [ValidateAntiForgeryToken]
        public ActionResult GetUserProfile(int id)
        {
            LibraryBLL.UserDTO userResult = LibraryCommon.SessionHelper.GetObjectFromJson<LibraryBLL.UserDTO>(HttpContext.Session, "user");
            if (userResult is not null)
            {
                ViewBag.CurrentUser = userResult;

                if ((!(userResult.RoleName == "Patron")) && (!(userResult.UserId == id)))
                {
                    RedirectToAction("Index", "Home");
                }
            }

            UserOperations userOperations = new UserOperations();
            // UserDTO userDto = new UserDTO();
            UserModel userModel = new UserModel();

            UserDTO userDto = userOperations.GetUserProfile(id);

            ModelState.Clear();

                userModel.UserId = userDto.UserId;
                userModel.UserName = userDto.UserName;
                userModel.IsActive = userDto.IsActive;
                userModel.RoleName = userDto.RoleName;
                userModel.FirstName = userDto.FirstName;
                userModel.LastName = userDto.LastName;
                userModel.Email = userDto.Email;

            return View(userModel);
        }

        // GET: UserController/Details/5
        [HttpGet]
        // [ValidateAntiForgeryToken]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        [HttpGet]
        // [ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel model)
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
                UserDTO user = new UserDTO(model.UserName, model.Password, model.IsActive.ToString(), model.RoleName, model.FirstName, model.LastName, model.Email, model.UserId);

                return View();
            }
            else
            {
                return View();
            }
        }

        // GET: UserController/UpdateUser/5
        public ActionResult UpdateUser(int id)
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

            RoleOperations roleOperations = new RoleOperations();
            List<RoleDTO> roleDtoList = roleOperations.GetRoles();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "roles", roleDtoList);
            ViewBag.RoleList = roleDtoList;
            
            UserOperations userOperations = new UserOperations();
            UserDTO userDto = userOperations.GetUsers().Find(item => item.UserId == id);

            bool result = userOperations.UpdateUser(userDto);

            if (result)
            {
                UserModel userModel = new UserModel();
                userModel.UserId = userDto.UserId;
                userModel.UserName = userDto.UserName;
                userModel.IsActive = userDto.IsActive;
                userModel.RoleName = userDto.RoleName;
                userModel.FirstName = userDto.FirstName;
                userModel.LastName = userDto.LastName;
                userModel.Email = userDto.Email;

                return View(userModel);
            }
            else
            {
                return View();
            }
        }

        // POST: UserController/UpdateUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(int userId, UserModel model)
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

            try
            {
                UserOperations userOperations = new UserOperations();

                UserDTO userDto = new UserDTO(model.UserName, model.Password, model.IsActive, model.RoleName, model.FirstName, model.LastName, model.Email, model.UserId);

                userOperations.UpdateUser(userDto);

                return RedirectToAction("GetUsers");
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        // GET: UserController/UpdateUserProfile/5
        [HttpGet]
        // [ValidateAntiForgeryToken]
        public ActionResult UpdateUserProfile(int id)
        {
            LibraryBLL.UserDTO userResult = LibraryCommon.SessionHelper.GetObjectFromJson<LibraryBLL.UserDTO>(HttpContext.Session, "user");
            if (userResult is not null)
            {
                ViewBag.CurrentUser = userResult;

                if ((!(userResult.RoleName == "Patron")) && (!(userResult.UserId == id)))
                {
                    RedirectToAction("Index", "Home");
                }
            }

            UserOperations userOperations = new UserOperations();
            UserDTO userDto = userOperations.GetUsers().Find(item => item.UserId == id);

            bool result = userOperations.UpdateUserProfile(userDto);

            if (result)
            {
                UserModel userModel = new UserModel();
                userModel.UserId = userDto.UserId;
                userModel.UserName = userDto.UserName;
                userModel.IsActive = userDto.IsActive;
                userModel.RoleName = userDto.RoleName;
                userModel.FirstName = userDto.FirstName;
                userModel.LastName = userDto.LastName;
                userModel.Email = userDto.Email;

                return View(userModel);
            }
            else
            {
                return View();
            }
        }

        // POST: UserController/UpdateUserProfile/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUserProfile(int userId, UserModel model)
        {
            LibraryBLL.UserDTO userResult = LibraryCommon.SessionHelper.GetObjectFromJson<LibraryBLL.UserDTO>(HttpContext.Session, "user");
            ViewBag.CurrentUser = userResult;

            if ((!(userResult.RoleName == "Patron")) || (!(userResult.UserId == userId)))
            {
                RedirectToAction("Index", "Home");
            }

            try
            {
                UserOperations userOperations = new UserOperations();

                UserDTO userDto = new UserDTO(model.UserName, model.Password, model.IsActive, model.RoleName, model.FirstName, model.LastName, model.Email, model.UserId);

                userOperations.UpdateUserProfile(userDto);

                RedirectToAction("GetUserProfile", new { id = userResult.UserId });
            }
            catch (Exception ex)
            {
                return View();
            }

            return View();

        }
    }
}
