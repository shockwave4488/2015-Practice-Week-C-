using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;

namespace _2015_Pre_build_week_project
{
    class Shifting
    {
        bool lowGear;
         
        public bool LowGear
        {
            get
            {
                return lowGear;
            }
            set
            {
                lowGear = value;
            }
        }

        /*

            public bool getLowGear()
            {
                return lowGear;
            }

            public void setLowGear(bool value)
            {
                lowGear = value;
            }
    */

        public Shifting(Boolean lGear)
        {
            lowGear = lGear;
            
        }

        public void update()
        {
            double voltage = DriverStation.Instance.GetBatteryVoltage();

            if( voltage < Constants.lowThresh )
            {
                lowGear = true;
            }

            else if( voltage > Constants.highThresh )
            {
                lowGear = false; 
            }
         
        }

       
        
        //Parameters
           //lower
               //Type: System.Double
               //The lower voltage limit.
           //upper
               //Type: System.Double
               //The higher voltage limit.            
    }
}
