using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;
using _2015_Pre_build_week_project.SubSystems;
using _2015_Pre_build_week_project.Team_Code.Drive_Code;
using _2015_Pre_build_week_project.Autonomous;
using WPILib.SmartDashboards;

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
        public static Roller roller;

        AutonScheduler scheduler;

        private DriveHelper TeleopDrive;

        static BuildWeek2015()
        {

        }

        /**
         * This function is run when the robot is first started up and should be
         * used for any initialization code.
         */
        public override void RobotInit()
        {
            drive = new Drive();
            primary = new Controllers();
            roller = new Roller();
            conveyer = new Conveyer();
            TeleopDrive = new DriveHelper(ref drive);
        }

        public override void AutonomousInit()
        {
            int routine = (int)SmartDashboard.GetNumber("AutoMode", 4);
            Console.WriteLine($"Running Sequence {routine}");
            switch (routine)
            {
                case 1:
                    scheduler = new AutonScheduler(AutonRoutines.Straight);
                    break;
                case 2:
                    scheduler = new AutonScheduler(AutonRoutines.Angled);
                    break;
                case 3:
                    scheduler = new AutonScheduler(AutonRoutines.UTurn);
                    break;
                default:
                    scheduler = new AutonScheduler(AutonRoutines.DoNothing);
                    break;
            }

        }

        /**
         * This function is called periodically during autonomous
         */
        public override void AutonomousPeriodic()
        {
            scheduler.Run();
        }

        /**
         * This function is called periodically during operator control
         */
        public override void TeleopPeriodic()
        {
            if (null == roller || null == drive || null == conveyer)
                Console.WriteLine($"Roller: {null == roller} Conveyer: {null == conveyer} Drive: {null == drive}");
            SmartDashboard.PutNumber("Goats", conveyer.balls);
            roller.ConveyerFull = conveyer.balls >= 6;
            roller.Reverse = primary.ReverseIntake;
            roller.Intake = primary.IntakeButton;
            conveyer.Output = primary.ConveyerPowerButton;

            if (conveyer.balls >= 6)
                roller.Force(false);

            TeleopDrive.Drive(primary.GetSpeed, primary.GetTurn, false, primary.ShiftLow, primary.ShiftHigh, false);
            conveyer.Update();
            roller.Update();
        }

        /**
         * This function is called periodically during test mode
         */
        public override void TestPeriodic()
        {

        }
    }
}
