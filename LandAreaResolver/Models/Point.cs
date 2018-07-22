using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandAreaResolver.Models
{
    [Serializable()]
    public class Point 
    {
        public double X { get; set; }
        public double Y { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Point p = (Point)obj;
            return (X == p.X) && (Y == p.Y);
        }

        public override int GetHashCode() => (int)X ^ (int)Y;

    }
}