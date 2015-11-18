using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;

namespace _2015_Pre_build_week_project.Team_Code
{
    /// <summary>
    /// A subclass of a motor controller intended to allow controlled change in power, for safety or voltage regulation means.
    /// </summary>
    public class RampingTalon : Talon //Add a new class if somebody needs to ramp a different kind of motor controller because PWM.Set(double) doesn't exist
    {
        private double power;

        /// <summary>
        /// The max amount of change in power the motor can accelerate with
        /// </summary>
        public double MaxAccel { get; set; }

        /// <summary>
        /// The max amount of change in power the motor can decellerate with
        /// </summary>
        public double MaxDecel { get; set; }

        /// <summary>
        /// Sets both MaxAccel and MaxDecel
        /// </summary>
        public double MaxChange
        {
            set { MaxAccel = value; MaxDecel = value; }
        }

        /// <summary>
        /// Opens a new RampingMotor 
        /// </summary>
        /// <param name="channel"></param>
        public RampingTalon(int channel) : base(channel)
        {
            MaxChange = 1;
        }

        /// <summary>
        /// Sets the value to the motor, within the change limitations
        /// </summary>
        /// <param name="value">value to set to or approach</param>
        public override void Set(double value)
        {
            //The motor is DECELLERATING if |value| < |power| OR value and power are not both positive or both negative
            //Likewise, the motor is ACCELERATING if |value| > |power| AND value and power are both negative or both positive
            //if the motor is ACCELERATING, use MaxAccel. If the robot is DECELLERATING, use MaxDecel.
            double delta = Math.Sign(value) == Math.Sign(power) && Math.Abs(value) > Math.Abs(power) ? MaxAccel : MaxDecel;

            if (value > power + delta) //If the motor wants to change power faster than it is allowed, change it by the max power change allowed
                power += delta;
            else if (value < power - delta)
                power -= delta;
            else //If the motor wants to go to a power within the change limitations, set the power to the value.
                power = value;

            base.Set(value);
        }

        /// <summary>
        /// Set the power of a motor regardless of the ramping limitations
        /// </summary>
        /// <param name="value">Power to set to</param>
        public void ForcePower(double value)
        {
            power = value;
        }
    }
}
