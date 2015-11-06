using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Pre_build_week_project
{
    public class MotorRamping
    {
        double Primary, Secondary;
        public double RampTime { get; set; }

        public double PrimarySpeed
        {
            get { return Primary; }
            //set { Primary = value; }
        }
        public double SecondarySpeed
        {
            get { return Secondary; }
            //set {Secondary = value;}
        }  


        public MotorRamping()
        {
            Primary = 0;
            Secondary = 0;
        }

        public void update(double speed)
        {

            if (speed > Primary + Constants.maxChange)
                Primary += Constants.maxChange;
            else if (speed < Primary - Constants.maxChange)
                Primary -= Constants.maxChange;
            else Primary = speed;

            if (speed > Secondary + (1.0 / 50.0 * RampTime))
                Secondary += (1.0 / 50.0 * RampTime);
            else if (speed < Secondary - (1.0 / 50.0 * RampTime))
                Secondary -= (1.0 / 50.0 * RampTime);
            else Secondary = speed;
        }
    }
}
