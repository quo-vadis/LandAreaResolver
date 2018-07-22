using LandAreaResolver.Logger.Helpers;
using LandAreaResolver.Logger.Interfaces;
using LandAreaResolver.Models;
using LandAreaResolver.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LandAreaResolver.Controllers
{
    public class CoordinatesController : Controller
    {
        private readonly ICoordinatesRepository _repo;
        private readonly ILogger _logger;
        private const string _datetimeFormat = "yyyy-MM-dd HH:mm:ss.fff";

        public CoordinatesController(ICoordinatesRepository repo)
        {
            _repo = repo;
            _logger = LoggerProviderFactory.GetProvider(LoggingProviders.Console);
        }

        [HttpPost]
        public ActionResult Add(double? xCoord, double? yCoord)
        {
            Point newPoint = new Point { X = xCoord ?? 0, Y = yCoord ?? 0 };
            bool pointAdded =_repo.Add(newPoint);
            if (pointAdded)
            {
                _logger.Info("new point added");
            }
           
            return RedirectToAction("Index", "Home", _repo.GetAll());
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            return Json(_repo.GetAll() ?? null, JsonRequestBehavior.AllowGet);
        }

    }
}