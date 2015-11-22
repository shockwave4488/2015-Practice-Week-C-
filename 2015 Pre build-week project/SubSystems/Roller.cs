using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;
using _2015_Pre_build_week_project.Team_Code.Utility;

namespace _2015_Pre_build_week_project.SubSystems
{
    public class Roller
    {
        private Talon motor;
        private Toggle on;

        public bool Intake
        {
            get
            {
                return on.state;
            }
            set
            {
                on.state = value;
            }
        }
        public bool Reverse { get; set; }
        public bool ConveyerFull { get; set; }

        public void Force(bool value)
        {
            on.Force(value);
        }

        public Roller()
        {
            on = new Toggle();
            motor = new Talon(Constants.Intake_Channel);
        }

        public void Update()
        {
            double value = 0;
            if (ConveyerFull)
                value = 0.3;
            else if (Reverse)
                value = 1;
            else if (Intake)
                value = -1;

            motor.Set(value);
        }
    }
}
