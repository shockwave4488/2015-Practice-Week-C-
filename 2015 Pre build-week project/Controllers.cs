using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;

namespace _2015_Pre_build_week_project.SubSystems
{
    public class Controllers
    {
        private Joystick xbox;

        public Controllers()
        {
            xbox = new Joystick(Constants.ControllerPort);
        }
<<<<<<< HEAD
        public double GetLeft()
        {
            return xbox.GetRawAxis(1);
        }
        public double GetRight()
        {
            return xbox.GetRawAxis(5);
        }
        public bool ConveyerPowerButton()
        {
            return xbox.GetRawButton(Constants.XboxA);
        }
        public bool GearShiftButton()
        {
            return xbox.GetRawButton(Constants.XboxB);
        }
        public bool RollerPowerButton()
        {
            return xbox.GetRawButton(Constants.XboxX);
        }
=======

        public double GetTurn => xbox.GetRawAxis(4);
        public double GetSpeed => xbox.GetRawAxis(1);

        public bool ConveyerPowerButton => xbox.GetRawButton(Constants.XboxA);

        public bool ShiftLow => xbox.GetRawButton(Constants.XboxLB);
        public bool ShiftHigh => xbox.GetRawButton(Constants.XboxRB);
        
>>>>>>> refs/remotes/origin/master
        public void Vibrate()
        {
            if (ControllerPower.GetInputVoltage() < Constants.VibrateVoltLimit)
            {
                xbox.SetRumble(Joystick.RumbleType.LeftRumble, 1);
                xbox.SetRumble(Joystick.RumbleType.RightRumble, 1);
            }
            else
            {
                xbox.SetRumble(Joystick.RumbleType.LeftRumble, 0);
                xbox.SetRumble(Joystick.RumbleType.RightRumble, 0);
            }

        }
    }
}
