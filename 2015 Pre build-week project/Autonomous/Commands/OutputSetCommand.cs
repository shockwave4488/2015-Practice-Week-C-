﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Pre_build_week_project.Autonomous.Commands
{
    public class OutputSetCommand : CommandBase
    {
        private bool setTo;

        public OutputSetCommand(bool on)
        {
            setTo = on;
            TimeOut = 0.1;
        }

        public override bool Execute()
        {
            conveyer.Output = setTo;
            base.Execute();
            return true;
        }
    }
}
