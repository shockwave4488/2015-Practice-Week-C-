using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Pre_build_week_project.Autonomous.Commands
{
    public class IntakeSetCommand : CommandBase
    {
        private bool on;

        public IntakeSetCommand(bool _on)
        {
            on = _on;
            TimeOut = 0.1;
        }

        public override bool Execute()
        {
            roller.Force(on);
            base.Execute();
            return true;
        }
    }
}
