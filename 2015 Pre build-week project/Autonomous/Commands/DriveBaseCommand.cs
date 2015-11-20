using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2015_Pre_build_week_project.SubSystems;
using _2015_Pre_build_week_project.Team_Code.Utility;

namespace _2015_Pre_build_week_project.Autonomous.Commands
{
    public abstract class DriveBaseCommand : AutonCommand
    {
        public double TimeOut { get; set; }
        protected double distance;
        protected double speed;
        public Drive drive = BuildWeek2015.drive;
        protected PID movePID;
        protected PID CorrectionPID;

        public DriveBaseCommand(double _distance, double _speed, double _TimeOut)
        {
            distance = _distance;
            speed = _speed;
            TimeOut = _TimeOut;

            movePID = new PID(Constants.Auton_Move_P, 0, Constants.Auton_Move_D, -0.2, 0.2);

            drive.ResetEncoders();
            drive.Update(0, 0, false);
        }

        public abstract bool Execute();
    }
}
