using System;
using System.Collections.Generic;

namespace _2015_Pre_build_week_project.Autonomous
{
    public class AutonScheduler
    {
        private Queue<AutonCommand> commands;
        private DateTime TimeOut;

        public bool finished => commands.Count == 0;

        public AutonScheduler(Queue<AutonCommand> _commands)
        {
            commands = _commands;
            TimeOut = DateTime.Now.AddSeconds(commands.Peek().TimeOut);
        }

        public void Run()
        {
            if (!finished && (commands.Peek().Execute() || DateTime.Now > TimeOut))
            {
                commands.Dequeue();
                TimeOut = DateTime.Now.AddSeconds(commands.Peek().TimeOut);
            }
        }
    }
}
