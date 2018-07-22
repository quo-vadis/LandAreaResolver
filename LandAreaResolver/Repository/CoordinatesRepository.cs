using LandAreaResolver.Logger.Helpers;
using LandAreaResolver.Logger.Interfaces;
using LandAreaResolver.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace LandAreaResolver.Repository
{
    public class CoordinatesRepository : ICoordinatesRepository
    {
        private readonly ILogger _logger = LoggerProviderFactory.GetProvider(LoggingProviders.File);
        private readonly string _logFileName;

        public CoordinatesRepository()
        {
            string fileName = System.Configuration.ConfigurationManager.AppSettings["CoordinatesFileName"];
            _logFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }       

        public bool Add(Point point)
        {
            List<Point> points = GetAll();
            if (points != null)
                points.Add(point);
            else
                points = new List<Point>() { point };

            try
            {
                using (var stream = new FileStream(_logFileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, points);
                    return true;
                }
            }
            catch (IOException ex)
            {
                _logger.Error("Error on adding point. Error: " + ex);
                return false;
            }
        }

        public void Delete()
        {
            if (File.Exists(_logFileName))
            {
                System.IO.File.WriteAllText(_logFileName, string.Empty);
            }
        }

        public List<Point> GetAll()
        {
            try
            {
                if (File.Exists(_logFileName))
                {
                    using (var stream = new FileStream(_logFileName, FileMode.Open, FileAccess.Read))
                    {
                        if (stream.CanRead && stream.Length > 0)
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            var points = (List<Point>)formatter.Deserialize(stream);
                            return points;
                        }
                        else
                        {
                            _logger.Error("File is empty.");
                        }
                    }
                }
            }
            catch (IOException)
            {
                _logger.Error($"Can't open file. File location {_logFileName}.");
                return null;
            }

            return null;
        }

        public bool Save(List<Point> list)
        {         
            try
            {
                if (!File.Exists(_logFileName))
                {
                    using (var stream = File.Open(_logFileName, FileMode.Create))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(stream, list);
                        return true;
                    }
                }
                else
                {
                    //if file exists then append new point
                    using (var stream = File.Open(_logFileName, FileMode.Create))
                    {
                        var points = GetAll();
                        if(points != null)
                            points.Add(list[0]);

                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(stream, list);
                        return true;
                    }
                }
            }
            catch (IOException)
            {
                _logger.Error($"Can't open file. File location {_logFileName}.");
                return false;
            }

        }
    }
}