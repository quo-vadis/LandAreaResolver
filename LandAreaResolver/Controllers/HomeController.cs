using LandAreaResolver.Logger.Helpers;
using LandAreaResolver.Logger.Interfaces;
using LandAreaResolver.Models;
using LandAreaResolver.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Mvc;

namespace LandAreaResolver.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICoordinatesRepository _repo;
        private readonly ILogger _logger;
        private readonly string _logFilePath;

        public HomeController(ICoordinatesRepository repo)
        {
            _repo = repo;
            _logger = LoggerProviderFactory.GetProvider(LoggingProviders.File);

            string fileName = System.Configuration.ConfigurationManager.AppSettings["LogFileName"];
            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        public ActionResult Index()
        {           
            var points = _repo.GetAll();            

            if (points != null && points.Count > 2)
            {               
                @ViewData["Area"] = GetArea(points);
            }

            _logger.Info("Open Home/Index page");
            return View(points);
        }

        public ActionResult Logging(string searchQuery)
        {
            _logger.Info("Open Home/Logging page");

            var logs = GetAllLogs();

            if (logs != null)
            {
                if (String.IsNullOrEmpty(searchQuery))
                {
                    return View(logs);
                }
                else
                {                    
                    return View(logs.FindAll(x => x.Contains(searchQuery)));
                }
            }
            return View();
        }

        public ActionResult Delete()
        {
            _repo.Delete();
            return RedirectToAction("Index", "Home");
        }

        public List<string> GetAllLogs()
        {
            var logs = new List<string>();
            if (System.IO.File.Exists(_logFilePath))
            {
                using (var reader = new StreamReader(_logFilePath))
                {
                    while (reader.Peek() >= 0)
                    {
                        String line = reader.ReadLine();
                        var index1 = line.IndexOf(' ');
                        var index2 = line.IndexOf(' ', index1 + 1);
                        var index3 = line.IndexOf(' ', index2 + 1);
                        logs.Add(line.Substring(index3));
                    }
                }
                logs.Reverse();
                return logs;
            }
            return null;
        }

        public double GetArea(List<Point> points)
        {
            double sum = 0;
            var localPoints = new List<Point>(points);
            if (localPoints[0] != localPoints[localPoints.Count - 1])
            {
                // last point must be equal to first one
                localPoints.Add(localPoints[0]);
                _logger.Info("To calculate land area added first point to end of collection");
            }

            _logger.Info("Start to count land area");
            for (int i = 0; i < localPoints.Count - 1; i++)
            {
                sum += (localPoints[i].X + localPoints[i + 1].X) * (localPoints[i].Y - localPoints[i + 1].Y);
            }

            #region formulas from task doesn't work correctly
            /*for ( int i = 1; i < points.Count-1; i++)
            {
                sum1 += points[i].Y * (points[i - 1].X - points[i + 1].X);
                sum2 += points[i].X * (points[i + 1].Y - points[i - 1].Y);                
            } */
            #endregion

            _logger.Info("Land area equals " + sum * .5);
            return Math.Abs(sum * .5);
        }
    }
}