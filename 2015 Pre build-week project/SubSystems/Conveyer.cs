using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;

namespace _2015_Pre_build_week_project.SubSystems
{
    class Conveyer
    {
        private Talon conveyor;
        private DigitalInput beamBrakeOut;
        private DigitalInput beamBrakeIn;
        public int balls { get; private set; }
        private bool InState;
        public Conveyer()
        {
            conveyor = new Talon(Constants.Conveyer_Channel);
            beamBrakeIn = new DigitalInput(Constants.Conveyer_BeambrakeInChannel);
            beamBrakeOut = new DigitalInput(Constants.Conveyer_BeambrakeInChannel);
            balls = 0;
            InState = false;

        }
        public void Update(bool output)
        {
            if (beamBrakeIn.Get() || beamBrakeOut.Get() || output)
            {
                conveyor.Set(Constants.Conveyer_speed);
                if (output)
                {
                    balls = 0;
                }
            }
            else
            {
                conveyor.Set(0);
            }
            if (InState &&!beamBrakeIn.Get())
            {
                balls++;
            }
}


    }
}
