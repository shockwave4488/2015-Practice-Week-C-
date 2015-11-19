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
    public class DriveForwardCommand : AutonCommand
    {
        Drive drive;
        double distance;
        double speed;
        PID straightCorrection;

        public double TimeOut { get; set; }

        public DriveForwardCommand(double _distance, double _speed, double timeOut)
        {
            distance = _distance;
            speed = _speed;
            TimeOut = timeOut;

            drive.ResetEncoders();
            drive.Update(0, 0, false);
            straightCorrection = new PID(0.1, 0, 0, -0.1, 0.1);
        }

        bool AutonCommand.Execute()
        {
            bool finished = drive.LinearDist > distance;

            straightCorrection.Update(drive.TurnDist);

            if (finished)
                drive.Update(0, 0); //Stop the motors if we are done
            else
                drive.Update(speed - straightCorrection.Get(drive.TurnDist), speed + straightCorrection.Get(drive.TurnDist));

            return finished;
        }
    }
}
