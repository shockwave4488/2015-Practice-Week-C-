using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;

namespace _2015_Pre_build_week_project.SubSystems
{
    /// <summary>
    /// Describes the drive train of this particular robot
    /// 6 wheel, 6 CIM two-speed tank drive
    /// </summary>
    public class Drive
    {
        Shifting Shift;
        public Drive()
        {
            Shift = new Shifting(true);
            Shift.LowGear = false;
        }
        MotorRamping left;
        MotorRamping right;

        double Speed;
        double Turn;

        public void Update()
        {
            left.update();
            right.update();
        }

        public Drive()
        {
            left = new MotorRamping();
            right = new MotorRamping();
        }
    }
}
