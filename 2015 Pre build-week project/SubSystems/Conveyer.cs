using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;
using _2015_Pre_build_week_project.Team_Code.Utility;

namespace _2015_Pre_build_week_project.SubSystems
{
    public class Conveyer
    {
        private Talon conveyor;
        private DigitalInput beamBrakeOut;
        private DigitalInput beamBrakeIn;

        private bool InState;

        public int balls { get; private set; }
        public bool Output { get; set; }

        public Conveyer()
        {
            conveyor = new Talon(Constants.Conveyer_Channel);
            beamBrakeIn = new DigitalInput(Constants.Conveyer_BeambrakeInChannel);
            beamBrakeOut = new DigitalInput(Constants.Conveyer_BeambrakeOutChannel);
            balls = 0;
            InState = false;

        }
        public void Update()
        {
            //Console.WriteLine($"Outer: {beamBrakeOut.Get()} Inner: {beamBrakeIn.Get()}");
            if (!beamBrakeIn.Get() || !beamBrakeOut.Get() || Output)
            {
                conveyor.Set(-Constants.Conveyer_speed);
                if (Output)
                {
                    balls = 0;
                }
            }
            else
            {
                conveyor.Set(0);
            }
            if (InState && beamBrakeIn.Get())
            {
                balls++;
            }

            InState = !beamBrakeIn.Get();
        }


    }
}
