using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2015_Pre_build_week_project.SubSystems;
using _2015_Pre_build_week_project.Team_Code.Utility;

namespace _2015_Pre_build_week_project.Autonomous.Commands
{
    public abstract class DriveBaseCommand : CommandBase
    {
        protected double distance;
        protected PID movePID;
        protected PID CorrectionPID;

        public DriveBaseCommand(double _distance, double _TimeOut)
        {
            distance = _distance; //Add six inches, because of the stop condition of subclasses of execute.
            TimeOut = _TimeOut;

            movePID = new PID(Constants.Auton_Move_P, 0, Constants.Auton_Move_D, -0.5, 0.5);
            CorrectionPID = new PID(Constants.Auton_Correction_P, 0, Constants.Auton_Correction_D, -0.75, 0.75);

            movePID.setpoint = distance;
            CorrectionPID.setpoint = 0;

            drive.ResetEncoders();
            drive.Update(0, 0, false);
        }
    }
}
