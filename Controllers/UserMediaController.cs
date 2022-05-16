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
    public class UserMediaController : Controller
    {
        [HttpGet]
        [Route("UserMedia/GetUserMediaItems/{userId}")]
        public IActionResult GetUserMediaItems(int userId)
        {
            LibraryBLL.UserDTO userResult = LibraryCommon.SessionHelper.GetObjectFromJson<LibraryBLL.UserDTO>(HttpContext.Session, "user");
            if (userResult is not null)
            {
                ViewBag.CurrentUser = userResult;

                if (!(userResult.RoleName == "Administrator") || (!(userResult.UserId != userId)))
                {
                    RedirectToAction("Index", "Home");
                }
            }

            UserMediaOperations userMediaOperations = new UserMediaOperations();
            ModelState.Clear();

            List<UserMediaDTO> userMediaDtoList = new List<UserMediaDTO>();
            userMediaDtoList = userMediaOperations.GetUserMediaItems(userId);

            List<UserMediaModel> userMediaModels = new List<UserMediaModel>();

            foreach (UserMediaDTO item in userMediaDtoList)
            {
                UserMediaModel model = new UserMediaModel();

                model.UserId = item.UserId;
                model.MediaId = item.MediaId;
                model.MediaName = item.MediaName;
                model.MediaType = item.MediaType;

                if (!string.IsNullOrEmpty(item.CheckOutDate)) 
                {
                    model.CheckOutDate = item.CheckOutDate;
                }
                else
                {
                    model.CheckOutDate = null;
                }

                if (!string.IsNullOrEmpty(item.CheckInDate))
                {
                    model.CheckInDate = item.CheckInDate;
                }
                else
                {
                    model.CheckInDate = null;
                }

                if (!string.IsNullOrEmpty(item.HoldRequestDate))
                {
                    model.HoldRequestDate = item.HoldRequestDate;
                }
                else
                {
                    model.HoldRequestDate = null;
                }

                userMediaModels.Add(model);
            }

            return View(userMediaModels);
        }
    }
}
