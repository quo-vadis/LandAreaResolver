using LandAreaResolver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandAreaResolver.Repository
{
    public interface ICoordinatesRepository
    {       
        List<Point> GetAll();
        bool Save(List<Point> list);
        bool Add(Point list);
        void Delete();
    }
}
