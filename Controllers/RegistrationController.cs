using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMvcApp.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: RegistrationController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: RegistrationController
        public string Index()
        {
            return "Registration Index() method return.";
        }

        // GET: RegistrationController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}
        public string Details(int id=0, string name="HiThere", int numTimes=1)
        {
            return HtmlEncoder.Default.Encode($"Registration Details() method return {id}, {name}, {numTimes}.");
        }

        // GET: RegistrationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegistrationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegistrationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RegistrationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegistrationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RegistrationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
