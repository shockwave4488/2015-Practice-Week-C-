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
        private DigitalInput beamBrakOut;
        private DigitalInput beamBrakeIn;
        public Conveyer()
        {
            conveyor = new Talon(Constants.Conveyer_Channel);
            beamBrakeIn = new DigitalInput(Constants.Conveyer_BeambrakeInChannel);
            beamBrakeOut = new DigitalInput(Constants.Conveyer_BeambrakeInChannel);

        }

    }
}
