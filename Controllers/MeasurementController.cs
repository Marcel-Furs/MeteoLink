using MeteoLink.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeteoLink.Controllers
{
    [MeteoLinkV1Route]
    [ApiController]
    [Authorize]
    public class MeasurementController : Controller
    {
        // GET: MeasurementController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MeasurementController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MeasurementController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeasurementController/Create
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

        // GET: MeasurementController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MeasurementController/Edit/5
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

        // GET: MeasurementController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MeasurementController/Delete/5
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
