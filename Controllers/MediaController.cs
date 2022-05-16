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
    public class MediaController : Controller
    {
        private readonly ILogger<MediaController> _logger;

        public MediaController(ILogger<MediaController> logger)
        {
            _logger = logger;
        }

        // GET: MediaController
        [HttpGet]
        public IActionResult GetInventory()
        {
            MediaOperations operations = new MediaOperations();
            List<MediaDTO> mediaDtoList = new List<MediaDTO>();
            List<MediaModel> mediaModels = new List<MediaModel>();

            mediaDtoList = operations.GetInventory();

            ModelState.Clear();

            foreach (MediaDTO item in mediaDtoList)
            {
                MediaModel model = new MediaModel();

                model.MediaId = item.MediaId;
                model.Type = item.Type;
                model.Quantity = item.Quantity;
                model.Name = item.Name;
                mediaModels.Add(model);
            }
  
            return View(mediaModels);
        }

        [HttpPost]
        public IActionResult SearchInventoryByName(InventorySearchByNameViewModel viewModel)
        {
            //if (String.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated")))
            //{
            //    HttpContext.Session.SetString("IsAuthenticated", "false");
            //}

            MediaOperations operations = new MediaOperations();
            List<MediaDTO> mediaDtoList = new List<MediaDTO>();
            List<MediaModel> mediaModels = new List<MediaModel>();


            if (String.IsNullOrEmpty(viewModel.SearchString))
            {
                // Retrieve the complete inventory list.
                mediaDtoList = operations.GetInventory();
            }
            else
            {
                mediaDtoList = operations.SearchInventoryByName(viewModel.SearchString);
            }

            foreach (MediaDTO item in mediaDtoList)
            {
                MediaModel model = new MediaModel();
                
                model.MediaId = item.MediaId;
                model.Type = item.Type;
                model.Quantity = item.Quantity;
                model.Name = item.Name;
                mediaModels.Add(model);
            }
            //ViewBag.MediaInventory = mediaModels;

            return View();
            // return RedirectToPage("~/SearchResult");
            // return RedirectToAction("Index");
            // return RedirectToAction("Index","Media");
        }

        // GET: Media/AddMediaItem
        [HttpGet]
        public IActionResult AddInventoryItem()
        {
            return View();
        }

        // POST: Media/AddMediaItem
        [HttpPost]
        public ActionResult AddInventoryItem(MediaModel item)
        {
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        MediaOperations mediaOperations = new MediaOperations();

                        MediaDTO itemDto = new MediaDTO(item.Quantity, item.Type, item.Name, item.MediaId);
                        
                        if (mediaOperations.AddInventoryItem(itemDto))
                        {
                            ViewBag.Message = "Media item added successfully";
                        }
                    }

                    return View();
                }
                catch (Exception ex)
                {
                    // TODO: Handle Controller Exceptions here ?
                    return View();
                }
            }

            return View();
        }

        // GET: Media/EditMediaItem/<id>
        [HttpGet]
        public IActionResult UpdateInventoryItem(int id)
        {
            LibraryBLL.UserDTO userResult = LibraryCommon.SessionHelper.GetObjectFromJson<LibraryBLL.UserDTO>(HttpContext.Session, "user");
            if (userResult is not null)
            {
                ViewBag.CurrentUser = userResult;

                if (!(userResult.RoleName == "Administrator")||(!(userResult.RoleName == "Librarian")))
                {
                    RedirectToAction("Index", "Home");
                }
            }

            MediaOperations mediaOperations = new MediaOperations();
            MediaDTO mediaDto = mediaOperations.GetInventory().Find(item => item.MediaId == id);

            bool result = mediaOperations.UpdateInventoryItem(mediaDto);

            if (result)
            {
                MediaModel mediaModel = new MediaModel();
                mediaModel.MediaId = mediaDto.MediaId;
                mediaModel.Quantity = mediaDto.Quantity;
                mediaModel.Type = mediaDto.Type;
                mediaModel.Name = mediaDto.Name;

                return View(mediaModel);
            }
            else
            {
                return View();
            }
        }

        // POST: Media/UpdateInventoryItem/<id>
        [HttpPost]
        public IActionResult UpdateInventoryItem(int id, MediaModel model)
        {
            LibraryBLL.UserDTO userResult = LibraryCommon.SessionHelper.GetObjectFromJson<LibraryBLL.UserDTO>(HttpContext.Session, "user");
            if (userResult is not null)
            {
                ViewBag.CurrentUser = userResult;

                if (!(userResult.RoleName == "Administrator") || (!(userResult.RoleName == "Librarian")))
                {
                    RedirectToAction("Index", "Home");
                }
            }

            try
            {
                MediaOperations mediaOperations = new MediaOperations();
                MediaDTO mediaDto = new MediaDTO(model.Quantity,model.Type,model.Name,model.MediaId);

                mediaOperations.UpdateInventoryItem(mediaDto);

                return RedirectToAction("GetInventory");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
    }
}
