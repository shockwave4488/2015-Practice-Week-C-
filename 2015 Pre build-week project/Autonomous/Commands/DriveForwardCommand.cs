using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2015_Pre_build_week_project.SubSystems;
using _2015_Pre_build_week_project.Autonomous;
using _2015_Pre_build_week_project.Team_Code.Utility;

namespace _2015_Pre_build_week_project.Autonomous.Commands
{
    public class DriveForwardCommand : DriveBaseCommand
    {
        public DriveForwardCommand(double _distance, double timeOut)
            : base(_distance, timeOut)
        {

        }

        public override bool Execute()
        {
            bool finished = drive.LinearDist > distance;

            double speed = movePID.Get(drive.LinearDist);
            speed = (Math.Abs(speed) < 0.05) ? 0.05 * Math.Sign(speed) : speed;

            double correction = CorrectionPID.Get(drive.TurnDist);

            CorrectionPID.Update(drive.TurnDist);
            movePID.Update(drive.LinearDist);

            if (finished)
                drive.Update(0, 0); //Stop the motors if we are done
            else
                drive.Update(speed - correction, speed + correction);

            return finished;
        }
    }
}
