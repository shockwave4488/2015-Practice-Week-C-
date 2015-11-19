using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Pre_build_week_project.Autonomous.Commands
{
    abstract class DriveBaseCommand : AutonCommand
    {
        public double TimeOut { get; set; }
        protected double distance;
        protected double speed;

    }
}
