using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2015_Pre_build_week_project.SubSystems;

namespace _2015_Pre_build_week_project.Autonomous.Commands
{
    public abstract class CommandBase : AutonCommand
    {
        public double TimeOut { get; set; }
        protected static Drive drive = BuildWeek2015.drive;
        protected static Conveyer conveyer = BuildWeek2015.conveyer;
        protected static Roller roller = BuildWeek2015.roller;

        public virtual bool Execute()
        {
            conveyer.Update();
            roller.Update();
            return true;
        }
    }
}
