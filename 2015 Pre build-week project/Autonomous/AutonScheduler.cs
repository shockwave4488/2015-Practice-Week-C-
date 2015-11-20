using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Pre_build_week_project.Autonomous
{
    public class AutonScheduler
    {
        private Queue<AutonCommand> commands;
        public bool finished { get; private set; }

        public AutonScheduler(Queue<AutonCommand> _commands)
        {
            commands = _commands;
            finished = false;
        }

        public void Run()
        {
            if (!finished && commands.Peek().Execute())
            {
                commands.Dequeue();
                if (commands.Count == 0)
                    finished = true;
            }
        }
    }
}
