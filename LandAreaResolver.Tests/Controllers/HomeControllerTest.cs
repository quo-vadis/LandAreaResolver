using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LandAreaResolver;
using LandAreaResolver.Controllers;
using LandAreaResolver.Repository;
using Moq;
using System.Reflection;
using LandAreaResolver.Models;

namespace LandAreaResolver.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void GetArea_HomeController_Successed()
        {
            // Arrange
            var mockRepository = new Mock<ICoordinatesRepository>();
            var controller = new HomeController(mockRepository.Object);
            var points = new List<Point>
            {
                new Point { X = 2, Y =2},
                new Point { X = 5, Y =5},
                new Point { X = 2, Y =8},
            };

            // Act  
            var area = controller.GetArea(points);

            Assert.AreEqual(area, 9);
        }
      
    }
}
