using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Pre_build_week_project.Autonomous.Commands
{
    public class AutonReadyCommand : CommandBase
    {
        public override bool Execute()
        {
            roller.Force(false);
            conveyer.Output = false;
            drive.Update(0, 0, false);

            base.Execute();
            return true;
        }
    }
}
