using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2015_Pre_build_week_project.SubSystems;
using _2015_Pre_build_week_project.Team_Code.Utility;
using WPILib;

namespace _2015_Pre_build_week_project.Team_Code.Drive_Code
{
    //Shamelessly copy-pasted from Team 254©'s Robot Code©

    /// <summary>
    /// Does all calculations for driving the robot split-arcade style. Contains the drive object.
    /// </summary>
    public class DriveHelper
    {
        private Drive drive;
        double oldTurn, quickStopAccumulator, oldSpeed, oldWheel, negInertiaAccumulator;

        int lastShift;
        InputFilter throttleAccel, wheelAccel;
        bool isHighGear;

        public bool HighGear => isHighGear;

        public DriveHelper()
        {
            drive = new Drive();
            oldTurn = quickStopAccumulator = oldSpeed = oldWheel = negInertiaAccumulator = lastShift = 0;
            throttleAccel = new InputFilter(0);
            wheelAccel = new InputFilter(0);
            isHighGear = false;
        }

        /// <summary>
        /// Return the Contained drive train.
        /// </summary>
        public Drive DriveTrain { get { return drive; } }

        /// <summary>
        /// Does all calculations and subroutines for driving the robot split-arcade style. 
        /// </summary>
        /// <param name="speed">Linear Control</param>
        /// <param name="turn">Rotational Control</param>
        /// <param name="isQuickTurn">Allows less smooth, but faster turning. Will likely be turned on in low gear.</param>
        /// <param name="forceHigh">Force High Gear</param>
        /// <param name="forceLow">Force Low Gear</param>
        /// <param name="autoShift">Allows automatic shifting</param>
        public void Drive(double speed, double turn, bool isQuickTurn, bool forceHigh, bool forceLow, bool autoShift)
        {
            if (DriverStation.Instance.Autonomous)
            {
                return;
            }

            double wheelNonLinearity;

            turn = handleDeadband(turn, Constants.Drive_SpeedDeadzone);
            speed = handleDeadband(speed, Constants.Drive_TurnDeadzone);

            double negInertia = turn - oldTurn;
            oldTurn = turn;

            //dy/dx ~= (y2 - y1) / dx. In this case, Y is wheel velocity and X is time.
            wheelAccel.Update((drive.LinearSpeed - oldWheel) / Constants.Teleop_dT);
            oldWheel = drive.LinearSpeed;
            
            //In this case, Y is the throttle. X is still time.
            throttleAccel.Update((speed - oldSpeed) / Constants.Teleop_dT);
            oldSpeed = speed;

            //If throttle == 0 and quickturn is off, things get wierd. Trust me, I tried.
            isQuickTurn = isQuickTurn || speed == 0;

            if (isHighGear)
            {
                wheelNonLinearity = 0.6;
                // Apply a sin function that's scaled to make it feel better.
                turn = Math.Sin(Math.PI / 2.0 * wheelNonLinearity * turn)
                        / Math.Sin(Math.PI / 2.0 * wheelNonLinearity);
                turn = Math.Sin(Math.PI / 2.0 * wheelNonLinearity * turn)
                        / Math.Sin(Math.PI / 2.0 * wheelNonLinearity);
            }
            else
            {
                wheelNonLinearity = 0.5;
                // Apply a sin function that's scaled to make it feel better.
                turn = Math.Sin(Math.PI / 2.0 * wheelNonLinearity * turn)
                        / Math.Sin(Math.PI / 2.0 * wheelNonLinearity);
                turn = Math.Sin(Math.PI / 2.0 * wheelNonLinearity * turn)
                        / Math.Sin(Math.PI / 2.0 * wheelNonLinearity);
                turn = Math.Sin(Math.PI / 2.0 * wheelNonLinearity * turn)
                        / Math.Sin(Math.PI / 2.0 * wheelNonLinearity);
            }

            double leftPwm, rightPwm, overPower;
            double sensitivity;

            double angularPower;
            double linearPower;

            // Negative inertia!
            double negInertiaScalar;
            if (isHighGear)
            {
                negInertiaScalar = 5.0;
                sensitivity = Constants.Drive_Sensitivity;
            }
            else
            {
                if (turn * negInertia > 0)
                {
                    negInertiaScalar = 2.5;
                }
                else
                {
                    if (Math.Abs(turn) > 0.65)
                    {
                        negInertiaScalar = 5.0;
                    }
                    else
                    {
                        negInertiaScalar = 3.0;
                    }
                }
                sensitivity = Constants.Drive_Sensitivity;
            }
            double negInertiaPower = negInertia * negInertiaScalar;
            negInertiaAccumulator += negInertiaPower;

            turn = turn + negInertiaAccumulator;
            if (negInertiaAccumulator > 1)
            {
                negInertiaAccumulator -= 1;
            }
            else if (negInertiaAccumulator < -1)
            {
                negInertiaAccumulator += 1;
            }
            else
            {
                negInertiaAccumulator = 0;
            }
            linearPower = speed;

            // Quickturn!
            if (isQuickTurn)
            {
                if (Math.Abs(linearPower) < 0.2)
                {
                    double alpha = 0.1;
                    quickStopAccumulator = (1 - alpha) * quickStopAccumulator + alpha
                            * ((Math.Abs(turn) < 1.0) ? turn : Math.Sign(turn)) * 5;
                }
                overPower = 1.0;
                if (isHighGear)
                {
                    sensitivity = 1.0;
                }
                else
                {
                    sensitivity = 1.0;
                }
                angularPower = turn;
            }
            else
            {
                overPower = 0.0;
                angularPower = Math.Abs(speed) * turn * sensitivity - quickStopAccumulator;
                if (quickStopAccumulator > 1)
                {
                    quickStopAccumulator -= 1;
                }
                else if (quickStopAccumulator < -1)
                {
                    quickStopAccumulator += 1;
                }
                else
                {
                    quickStopAccumulator = 0.0;
                }
            }

            rightPwm = leftPwm = linearPower;
            leftPwm += angularPower;
            rightPwm -= angularPower;

            if (leftPwm > 1.0)
            {
                rightPwm -= overPower * (leftPwm - 1.0);
                leftPwm = 1.0;
            }
            else if (rightPwm > 1.0)
            {
                leftPwm -= overPower * (rightPwm - 1.0);
                rightPwm = 1.0;
            }
            else if (leftPwm < -1.0)
            {
                rightPwm += overPower * (-1.0 - leftPwm);
                leftPwm = -1.0;
            }
            else if (rightPwm < -1.0)
            {
                leftPwm += overPower * (-1.0 - rightPwm);
                rightPwm = -1.0;
            }

            //Shifting! (4488's own special addition)
            if (forceHigh ^ forceLow)
            {
                isHighGear = !forceHigh;
                lastShift = 0;
            }
            else if (lastShift < Constants.Drive_Shifting_Hysterisis || turn > Constants.Drive_Shifting_TurnThreshold || !autoShift)
            {
                //don't shift at all
            }
            else if (speed > (Constants.speedThresh * 1.2) && throttleAccel.GetValue() > Constants.Drive_Shifting_ThrottleAccelThreshold
                && wheelAccel.GetValue() > Constants.Drive_Shifting_WheelAccelThreshold)
            {
                isHighGear = true;
                lastShift = 0;
            }
            else if (drive.LinearSpeed < Constants.Drive_Shifting_LowSpeedThreshold || wheelAccel.GetValue() < Constants.Drive_Shifting_LowAccelThreshold
                || ControllerPower.GetInputVoltage() < Constants.lowThresh)
            {
                isHighGear = false;
                lastShift = 0;
            }

            lastShift += 20;

            drive.Update(leftPwm, rightPwm, isHighGear);
        }
        
        /// <summary>
        /// Handles the deadband in the controllers. For Xbox controllers, the deadband on the joysticks is about 0.2
        /// </summary>
        /// <param name="val">Input Value</param>
        /// <param name="deadband">deadband</param>
        /// <returns>If value is within deadband, 0, Otherwise value</returns>
        public double handleDeadband(double val, double deadband)
        {
            return (Math.Abs(val) > Math.Abs(deadband)) ? val : 0.0;
        }
    }
}
