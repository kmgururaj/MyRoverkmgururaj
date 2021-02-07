using System.Web.Mvc;
using System.Collections.Generic;
using Guru.DataProcessor;
using Guru.Rover.WebApplication.Controllers.Models;

namespace Guru.Rover.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.NumberOfRows = ApplicationConstants.NumberOfRows;
            ViewBag.NumberOfColumns = ApplicationConstants.NumberOfColumns;
            ViewBag.CommunicationLatenceInMilliseconds = ApplicationConstants.CommunicationLatenceInMilliseconds;

            return View();
        }

        [HttpGet]
        public JsonResult GetAllRoversInitialData()
        {
            Dictionary<string, DataFromCsv> data = AppSession.Handler.InitSession();
            var viweModel = data.GetInitialRoverPositions();

            return Json(viweModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetNextCommand()
        {
            var data = AppSession.Handler.CsvData;
            var viweModel = data.GetNextMoveCommand();
            return Json(viweModel, JsonRequestBehavior.AllowGet);
        }
    }
}