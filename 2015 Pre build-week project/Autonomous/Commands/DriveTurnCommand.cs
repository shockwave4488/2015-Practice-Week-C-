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
    public class DriveTurnCommand : DriveBaseCommand
    {
        public DriveTurnCommand(double _degrees, double _timeOut) 
            : base(Utility.ToRadians(_degrees) * Constants.Robot_Width / 2, _timeOut)
        {

        }

        public override bool Execute()
        {
            bool finished = drive.TurnDist > distance;

            double speed = movePID.Get(drive.TurnDist);
            speed = Math.Abs(speed) < 0.05 ? 0.05 * Math.Sign(speed) : speed;

            double correction = CorrectionPID.Get(drive.TurnDist);

            CorrectionPID.Update(drive.TurnDist);
            movePID.Update(drive.LinearDist);

            if (finished)
                drive.Update(0, 0); //Stop teh motors if we are done
            else
                drive.Update(correction - speed, correction + speed);

            return finished;
        }
    }
}
