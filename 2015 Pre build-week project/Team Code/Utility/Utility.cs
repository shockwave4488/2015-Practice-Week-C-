using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Pre_build_week_project.Team_Code.Utility
{
    public static class Utility
    {
        public static double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }
        public static double ToDegrees(double radians)
        {
            return radians * (180.0 / Math.PI);
        }
    }
}
