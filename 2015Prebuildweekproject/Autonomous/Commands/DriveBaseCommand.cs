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

        private bool firstExecute;

        public DriveBaseCommand(double _distance, double _TimeOut)
        {
            distance = _distance; //Add six inches, because of the stop condition of subclasses of execute.
            TimeOut = _TimeOut;

            movePID = new PID(Constants.Auton_Move_P, 0, Constants.Auton_Move_D, -0.125, 0.125);
            CorrectionPID = new PID(Constants.Auton_Correction_P, 0, Constants.Auton_Correction_D, -0.125, 0.125);

            movePID.setpoint = distance;
            CorrectionPID.setpoint = 0;

            firstExecute = true; 
        }

        public override bool Execute()
        {
            if (firstExecute)
            {
                drive.ResetEncoders();
                firstExecute = false;
            }
            return base.Execute();
        }

        public static void normalize(double limit, ref double val1, ref double val2)
        {
            if(Math.Abs(val1) > limit)
            {
                val1 /= val1;
                val1 *= limit * Math.Sign(val1);
                val2 = limit * Math.Sign(val2);
            }
            if(Math.Abs(val2) > limit)
            {
                val1 /= val2;
                val1 *= limit * Math.Sign(val1);
                val2 = limit * Math.Sign(val2);
            }
        }
    }
}
