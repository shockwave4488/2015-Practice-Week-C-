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
        PID turnPID;

        public double TimeOut { get; set; }

        public DriveTurnCommand(double _speed, double _degrees, double _timeOut) 
            : base(Utility.ToRadians(_degrees) * Constants.Robot_Width / 2, _speed, _timeOut)
        {
            turnPID = new PID(Constants.Auton_Turn_P, 0, Constants.Auton_Turn_D, -0.2, 0.2);
        }

        public override bool Execute()
        {
            return base.Execute();
        }
    }
}
