using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Pre_build_week_project.Autonomous.Commands
{
    public class OutputSetCommand : AutonCommand
    {
        public double TimeOut { get; set; }
        private bool setTo;

        public OutputSetCommand(bool on)
        {
            setTo = on;
        }

        public bool Execute()
        {
            BuildWeek2015.conveyer.Update(setTo);
            return true;
        }
    }
}
