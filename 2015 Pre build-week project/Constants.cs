using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Pre_build_week_project
{
    /// <summary>
    /// Defines all constant values in the robot program.
    /// </summary>
    public static class Constants
    {
        //Define all values as: System_Use_Name
        //Examples: Drive_PID_GyroCorrect | Joystick_Rumble_Scaling | Elevator_Setpoint_Low
        //Makes things easier to find, so long as one knows what system is being worked on.
        //Create a new region if you have a new category, try to keep them organized by system.

        #region PWM Channels

        #endregion

        #region Digital IO Channels

        #endregion

        #region Solenoid Channels

        #endregion

        #region Analogue IO Channels

        #endregion

        #region Joystick USB Channels

        #endregion

        #region VoltageThresholds
        public const double lowThresh = 8.0;
        public const double highThresh = 10.0;
        #endregion

        #region SpeedThresholds
        public const double speedThresh = 9.5;
        #endregion
    }
}
