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
        public const int Drive_L1Channel = 1;
        public const int Drive_L2Channel = 3;
        public const int Drive_L3Channel = 2; //Coast
        public const int Drive_R1Channel = 4;
        public const int Drive_R2Channel = 6;
        public const int Drive_R3Channel = 7; //Coast
        #endregion

        #region Digital IO Channels
        public const int Drive_LEncoderAChannel = 0;
        public const int Drive_LEncoderBChannel = 1;
        public const int Drive_REncoderAChannel = 8;
        public const int Drive_REncoderBChannel = 9;
        #endregion

        #region Solenoid Channels
        public const int Drive_LShiftChannel = 0;
        public const int Drive_RShiftChannel = 1;
        #endregion

        #region Analogue IO Channels
        public const int Drive_GyroChannel = 0;
        #endregion

        #region Joystick USB Channels and other control variables
        public const float VibrateVoltLimit = 8;
        public const int ControllerPort = 0;
        public const int XboxA = 0;
        public const int XboxB = 1;
        public const int XboxX = 2;
        public const int XboxY = 3;
        public const int XboxLB = 4;
        public const int XboxRB = 5;
        #endregion

        #region Drive Variables
        public const double maxChange = 0.2;
        public const double Drive_MaxPowerChange = 0.2;
        public const double Drive_SpeedDeadzone = 0.2;
        public const double Drive_TurnDeadzone = 0.2;
        public const double Drive_Sensitivity = 0.75;

        #region Auto-Shifting
        public const double speedThresh = 9.5;
        public const double lowThresh = 8.0;
        public const int Drive_Shifting_Hysterisis = 500;
        public const double Drive_Shifting_TurnThreshold = 0.5;
        public const double Drive_Shifting_ThrottleAccelThreshold = 0.1;
        public const double Drive_Shifting_WheelAccelThreshold = 1;
        public const double Drive_Shifting_LowSpeedThreshold = 8;
        public const double Drive_Shifting_LowAccelThreshold = -1;
        #endregion
        //end Drive Region
        #endregion

        #region flow control variables
        public const int Teleop_dT = 20;
        #endregion

        #region Misc. Variables
        public const double Robot_Width = 28;
        #endregion

        #region Autonomous Variables
        public const double Auton_Move_P = 0.1;
        public const double Auton_Move_D = 0;
        public const double Auton_Correction_P = 0.05;
        public const double Auton_Correction_D = 0;
        #endregion
    }
}
