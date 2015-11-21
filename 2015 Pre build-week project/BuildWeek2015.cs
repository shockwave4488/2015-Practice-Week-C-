using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;
using _2015_Pre_build_week_project.SubSystems;
using _2015_Pre_build_week_project.Team_Code.Drive_Code;

namespace _2015_Pre_build_week_project
{
    /**
     * The VM is configured to automatically run this class, and to call the
     * functions corresponding to each mode, as described in the IterativeRobot
     * documentation. 
     */
    public class BuildWeek2015 : IterativeRobot
    {
        public static Drive drive;
        public static Controllers primary;
        public static Conveyer conveyer;

        private DriveHelper TeleopDrive;
        /**
         * This function is run when the robot is first started up and should be
         * used for any initialization code.
         */
        public override void RobotInit()
        {
            drive = new Drive();
            primary = new Controllers();
            TeleopDrive = new DriveHelper(ref drive);
        }

        /**
         * This function is called periodically during autonomous
         */
        public override void AutonomousPeriodic()
        {

        }

        /**
         * This function is called periodically during operator control
         */
        public override void TeleopPeriodic()
        {
            TeleopDrive.Drive(primary.GetSpeed, primary.GetTurn, false, primary.ShiftLow, primary.ShiftHigh, false);
        }

        /**
         * This function is called periodically during test mode
         */
        public override void TestPeriodic()
        {

        }
    }
}
