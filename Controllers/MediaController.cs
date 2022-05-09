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
        public IActionResult Index()
        {
            MediaOperations operations = new MediaOperations();
            List<MediaDTO> mediaDtoList = new List<MediaDTO>();
            List<MediaModel> mediaModels = new List<MediaModel>();


            mediaDtoList = operations.GetInventory();

            foreach (MediaDTO item in mediaDtoList)
            {
                MediaModel model = new MediaModel();

                model.MediaId = item.MediaId;
                model.Type = item.Type;
                model.Quantity = item.Quantity;
                model.Name = item.Name;
                mediaModels.Add(model);
            }
            ViewBag.MediaInventory = mediaModels;

            return View();
        }
    }
}
