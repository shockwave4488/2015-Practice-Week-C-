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
            Console.WriteLine("Initializing Scheduler");
            commands = new Queue<AutonCommand>(_commands);
            if(!finished)
                TimeOut = DateTime.Now.AddSeconds(commands.Peek().TimeOut);
        }

        public void Run()
        {
            if (!finished && (commands.Peek().Execute() || DateTime.Now > TimeOut))
            {
                Console.WriteLine($"Running {commands.Peek().GetType().Name}");
                if(DateTime.Now > TimeOut)
                    Console.WriteLine($"{commands.Peek().GetType().Name} Timed Out!");
                else
                    Console.WriteLine($"{commands.Peek().GetType().Name} Finished successfully!");
                commands.Dequeue();
                if (!finished)
                    TimeOut = DateTime.Now.AddSeconds(commands.Peek().TimeOut);
            }
        }
    }
}
