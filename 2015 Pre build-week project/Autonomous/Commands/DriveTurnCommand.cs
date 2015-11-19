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
    public class DriveTurnCommand : AutonCommand
    {
        Drive drive;
        double speed;
        double distance;
        PID turnPID;

        public double TimeOut { get; set; }

        public DriveTurnCommand(double speed, double degrees)
        {
            
        }

        bool AutonCommand.Execute()
        {
            return false;
        }
    }
}
